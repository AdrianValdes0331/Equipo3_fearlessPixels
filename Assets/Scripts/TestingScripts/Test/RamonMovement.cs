using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class RamonMovement : MonoBehaviour
{
    public float MaxSpeed;
    public float Acceleration;
    public float JumpSpeed;
    public float JumpDuration;

    private float LastShoot;
    public static float ramon_health;
    public GameObject Bullet;
    private Animator Animator;
    private Rigidbody2D Rigidbody2D;
    private float Horizontal;


    public bool EnableDoubleJump = true;
    public bool wallHitDoubleJumpOverride = true;

    bool canDoubleJump = true;
    float jmpDuration;
    bool jumpKeyDown = false;
    bool canVariableJump = false;
    // Start is called before the first frame update
    void Start()
    {
        ramon_health = 1;
        Rigidbody2D = GetComponent<Rigidbody2D>();
        //Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        if (Horizontal < 0.0f)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else if (Horizontal > 0.0f)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        //Animator.SetBool("running", Horizontal != 0.0f);

        /*if (Input.GetKey(KeyCode.LeftShift) && Time.time > LastShoot + 0.25f)
        {
            Animator.SetTrigger("shoot");
            Shoot();
            LastShoot = Time.time;
        }*/

        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal < -0.1f)
        {
            if (GetComponent<Rigidbody2D>().velocity.x > -this.MaxSpeed)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-this.Acceleration, 0.0f));
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-this.MaxSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
        }
        else if (horizontal > 0.1f)
        {
            if (GetComponent<Rigidbody2D>().velocity.x < this.MaxSpeed)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(this.Acceleration, 0.0f));
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(this.MaxSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
        }

        bool onTheGround = isOnGround();

        //float vertical = Input.GetAxis("Vertical");

        if (onTheGround)
        {
            canDoubleJump = true;
        }

        if (Input.GetKeyDown(KeyCode.Space)/*vertical > 0.1f*/)
        {
            if (!jumpKeyDown)
            {
                jumpKeyDown = true;

                if (onTheGround || (canDoubleJump && EnableDoubleJump) || wallHitDoubleJumpOverride)
                {
                    bool wallHit = false;
                    int wallHitDirection = 0;

                    bool leftWallHit = isOnWallLeft();
                    bool rightWallHit = isOnWallRight();

                    if (horizontal != 0)
                    {
                        if (leftWallHit)
                        {
                            wallHit = true;
                            wallHitDirection = 1;
                        }
                        else if (rightWallHit)
                        {
                            wallHit = true;
                            wallHitDirection = -1;
                        }
                    }


                    if (!wallHit)
                    {
                        if (onTheGround || (canDoubleJump && EnableDoubleJump))
                        {
                            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, this.JumpSpeed);

                            JumpDuration = 0.0f;
                            canVariableJump = true;

                        }
                    }
                    else
                    {
                        GetComponent<Rigidbody2D>().velocity = new Vector2(this.JumpSpeed * wallHitDirection, this.JumpSpeed);

                        jmpDuration = 0.0f;
                        canVariableJump = true;
                    }
                    if (!onTheGround && !wallHit)
                    {
                        canDoubleJump = false;
                    }
                }
            }
            else if (canVariableJump)
            {
                jmpDuration += Time.deltaTime;

                if (jmpDuration < this.JumpDuration / 1000)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, this.JumpSpeed);
                }
            }
        }
        else
        {
            jumpKeyDown = false;
            canVariableJump = false;
        }

    }

    /*private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 100.0f)
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.left;
        }
        GameObject bullet = Instantiate(Bullet, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<Bullet_Movement>().SetDirection(direction);
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy_Bullet" || collision.gameObject.tag == "LV2_Punches")
        {
            ramon_health -= 0.1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Acid_Drops")
        {
            ramon_health -= 0.1f;
        }
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal, Rigidbody2D.velocity.y);
    }
    private bool isOnGround()
    {
        float lengthToSearch = 0.1f;
        float colliderThreshhold = 0.001f;
        Vector2 linestart = new Vector2(this.transform.position.x, this.transform.position.y - this.GetComponent<Renderer>().bounds.extents.y - colliderThreshhold);
        Vector2 vectorToSearch = new Vector2(this.transform.position.x, linestart.y - lengthToSearch);
        RaycastHit2D hit = Physics2D.Linecast(linestart, vectorToSearch);
        return hit;
    }

    private bool isOnWallLeft()
    {
        bool retVal = false;
        float lengthToSearch = 0.1f;
        float colliderThreshhold = 0.001f;
        Vector2 linestart = new Vector2(this.transform.position.x - this.GetComponent<Renderer>().bounds.extents.x - colliderThreshhold, this.transform.position.y);
        Vector2 vectorToSearch = new Vector2(linestart.x - lengthToSearch, this.transform.position.y);

        RaycastHit2D hitLeft = Physics2D.Linecast(linestart, vectorToSearch);
        retVal = hitLeft;
        if (retVal)
        {
            if (hitLeft.collider)
            {
                retVal = false;
            }
        }
        return retVal;
    }

    private bool isOnWallRight()
    {
        bool retVal = false;
        float lengthToSearch = 0.1f;
        float colliderThreshhold = 0.001f;
        Vector2 linestart = new Vector2(this.transform.position.x + this.GetComponent<Renderer>().bounds.extents.x + colliderThreshhold, this.transform.position.y);
        Vector2 vectorToSearch = new Vector2(linestart.x + lengthToSearch, this.transform.position.y);

        RaycastHit2D hitRight = Physics2D.Linecast(linestart, vectorToSearch);
        retVal = hitRight;
        if (retVal)
        {
            if (hitRight.collider)
            {
                retVal = false;
            }
        }
        return retVal;
    }
}