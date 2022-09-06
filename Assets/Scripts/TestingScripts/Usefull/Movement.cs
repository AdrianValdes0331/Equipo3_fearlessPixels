using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System;

public class Movement : MonoBehaviour
{
    public float MaxSpeed;
    public float JumpSpeed;

    private float dirX;

    private float LastShoot;
    public static float P1health;
    public GameObject Scope;
    public GameObject Misile;
    public Animator Animator;
    public Transform FirePoint;
    public Transform ScopeSpawn;
    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    public float pSize = 0.15f;

    public bool EnableDoubleJump = true;

    bool canDoubleJump = true;
    bool jumpKeyDown = false;

    // Start is called before the first frame update
    void Start()
    {
        P1health = 100;
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        MaxSpeed = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Player#1    
        P1movements();

        //Jump/doubleJump
        bool onTheGround = isOnGround();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump(onTheGround);
        }
        //Define ground to avoid infinite jump
        if (onTheGround)
        {
            canDoubleJump = true;
        }
        else
        {
            jumpKeyDown = false;
        }
    }

    //P1 Keyboard
    private void P1movements()
    {
        //Animation
        if (!Animator.GetCurrentAnimatorStateInfo(0).IsName("Chinmisil") || !GameObject.FindWithTag("scope"))
        {
            dirX = Input.GetAxisRaw("Horizontal") * MaxSpeed;
            Animator.SetBool("Chinmisil", false);
        }
        if (dirX != 0 && !Animator.GetCurrentAnimatorStateInfo(0).IsName("Chinkick") && !Animator.GetCurrentAnimatorStateInfo(0).IsName("brinco"))
        {
            Animator.SetBool("Walk", true);
        }
        else
        {
            Animator.SetBool("Walk", false);
        }

        //kick
        if (Input.GetKey(KeyCode.Z) && !Animator.GetCurrentAnimatorStateInfo(0).IsName("Chinkick"))
        {
            Animator.SetBool("Walk", false);
            Animator.SetTrigger("Chinkick");
        }

        //Misil
        if (Input.GetKey(KeyCode.X) && !Animator.GetCurrentAnimatorStateInfo(0).IsName("Chinmisil") && !Animator.GetCurrentAnimatorStateInfo(0).IsName("Walk") && !GameObject.FindWithTag("scope"))
        {
            Animator.SetBool("Walk", false);
            Animator.SetBool("Chinmisil", true);
            Shoot();
        }

        //Change direction
        Horizontal = Input.GetAxisRaw("Horizontal");
        //Move
        if (Horizontal < 0.0f)
        {
            transform.localScale = new Vector3(-pSize, pSize, pSize);
        }
        else if (Horizontal > 0.0f)
        {
            transform.localScale = new Vector3(pSize, pSize, pSize);
        }

    }

    //jump/dopublejump
    private void jump(bool onTheGround)
    {
        if (!jumpKeyDown)
        {
            jumpKeyDown = true;

            if (onTheGround || (canDoubleJump && EnableDoubleJump))
            {
                Animator.SetTrigger("brinco");
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, this.JumpSpeed);
            }

            if (!onTheGround)
            {
                canDoubleJump = false;
            }
        }
    }

    //Shoot misile
    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 0.15f)
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.left;
        }

        GameObject scope = Instantiate(Scope, ScopeSpawn.position, Quaternion.identity);
        GameObject misile = Instantiate(Misile, FirePoint.position, Quaternion.identity);
    }

    //Validate Ground
    private bool isOnGround()
    {
        float lengthToSearch = 0.1f;
        float colliderThreshhold = 0.001f;
        Vector2 linestart = new Vector2(this.transform.position.x, this.transform.position.y - this.GetComponent<Renderer>().bounds.extents.y - colliderThreshhold);
        Vector2 vectorToSearch = new Vector2(this.transform.position.x, linestart.y - lengthToSearch);
        RaycastHit2D hit = Physics2D.Linecast(linestart, vectorToSearch);
        return hit;
    }

    //ReduceHealth
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy_Punch")
        {
            P1health -= 1.0f;
        }
    }

    //Speed
    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(dirX, Rigidbody2D.velocity.y);
    }
}