using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class Generic_Movements : MonoBehaviour
{
    public float MaxSpeed;
    public float JumpSpeed;
    [HideInInspector] public float dirX;
    [HideInInspector] public Animator Animator;
    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    public float pSize = 0.15f;
    public bool EnableDoubleJump = true;
    public string AnimJumpName;
    public string AnimWalk;

    bool canDoubleJump = true;
    bool jumpKeyDown = false;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {          
        Movements();

        //Jump/doubleJump
        bool onTheGround = isOnGround();
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
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

    // Basic Movements
    private void Movements()
    {
        //Animation     
        dirX = Input.GetAxisRaw("Horizontal") * MaxSpeed;
        if (AnimJumpName != "" && AnimJumpName != "")
        {
            if (dirX != 0 && !Animator.GetCurrentAnimatorStateInfo(0).IsName(AnimJumpName))
            {
                Animator.SetBool(AnimWalk, true);
            }
            else
            {
                Animator.SetBool(AnimWalk, false);
            }
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

    // jump/dopublejump
    private void jump(bool onTheGround)
    {
        if (!jumpKeyDown)
        {
            jumpKeyDown = true;

            if (onTheGround || (canDoubleJump && EnableDoubleJump))
            {
                if (AnimJumpName != "")
                {
                    Animator.SetTrigger(AnimJumpName);
                }
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, this.JumpSpeed);
            }

            if (!onTheGround)
            {
                canDoubleJump = false;
            }
        }
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

    //Speed
    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(dirX, Rigidbody2D.velocity.y);
    }
}
