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
    public GameObject Bullet;
    private Animator Animator;
    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    public float pSize = 0.05f;

    public bool EnableDoubleJump = true;

    bool canDoubleJump = true;
    bool jumpKeyDown = false;
    
    // Start is called before the first frame update
    void Start()
    {
        P1health = 100;
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        MaxSpeed = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {

        //Player#1
        //Better to move to another script
        if (name == "PlayerOne")
        {
            //Change direction
            Horizontal = Input.GetAxisRaw("Horizontal");
            if (Horizontal < 0.0f)
            {
                transform.localScale = new Vector3(-pSize, pSize, pSize);
            }
            else if (Horizontal > 0.0f)
            {
                transform.localScale = new Vector3(pSize, pSize, pSize);
            }

            dirX = Input.GetAxisRaw("Horizontal") * MaxSpeed;
            //kick
            if (Input.GetKey(KeyCode.Z))
            {
                print("kick");
                Animator.SetTrigger("Chinkick");
            }
        }

        //Player#2
        if (name == "PlayerTwo" && Input.anyKey)
        {
            if (Input.GetKey(KeyCode.J))
            {
                dirX = -MaxSpeed;
                transform.localScale = new Vector3(-pSize, pSize, pSize);
                print("left");
            }
            if (Input.GetKey(KeyCode.L))
            {
                dirX = MaxSpeed;
                transform.localScale = new Vector3(pSize, pSize, pSize);
                print("right");
            }
            if (Input.GetKey(KeyCode.N))
            {
                print("kick");
                Animator.SetTrigger("Chinkick");
            }           
        }
        else if (name == "PlayerTwo" && !Input.anyKey)
        {
            print("none");
            dirX = 0.0f;
        }

        //Define ground to avoid infinite jump
        bool onTheGround = isOnGround();
        if (onTheGround)
        {
            canDoubleJump = true;
        }

        //Jump/doubleJump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!jumpKeyDown)
            {
                jumpKeyDown = true;
                
                if (onTheGround || (canDoubleJump && EnableDoubleJump))
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, this.JumpSpeed);
                }     
                else
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(this.JumpSpeed, this.JumpSpeed);
                }
                if (!onTheGround)
                {
                    canDoubleJump = false;
                }
            }
        }
        else
        {
            jumpKeyDown = false;         
        }
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

}