using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class NewMovement : MonoBehaviour
{
    public float MaxSpeed;
    public float JumpSpeed;
    public float RecoverySpeed;
    [HideInInspector] public float dirX;
    [HideInInspector] public Animator Animator;
    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    private float pSize;
    public bool EnableDoubleJump = true;
    public bool EnableRecovery = true;
    public string AnimJumpName = "none";
    public string AnimRecoveryName = "none";
    public string AnimWalk = "none";

    bool canDoubleJump = true;
    bool canRecovery = true;
    bool jumpKeyDown = false;
    bool recoveryKeyDown = false;
    [HideInInspector] public Vector2 i_movement; 

    // Start is called before the first frame update
    void Start()
    {
        pSize = transform.localScale.x;
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {          
        Movements();

        //Jump/doubleJump
        bool onTheGround = isOnGround();
        // if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
        // {
        //     jump(onTheGround);
        // }
        //Define ground to avoid infinite jump
        if (onTheGround)
        {
            canDoubleJump = true;
            canRecovery = true;
        }
        else
        {
            jumpKeyDown = false;
            recoveryKeyDown = false;
        }
    }

    public void OnMovement(InputValue val){

        i_movement = val.Get<Vector2>();
        Animator.SetBool(AnimRecoveryName, false);
        //Debug.Log(i_movement);

    }

    // Basic Movements
    private void Movements()
    {
        //Animation     
        dirX = i_movement.x * MaxSpeed;
        if(dirX != 0 && !Animator.GetCurrentAnimatorStateInfo(0).IsName(AnimJumpName) && !Animator.GetCurrentAnimatorStateInfo(0).IsTag("1"))
        {
            Animator.SetBool(AnimWalk, true);
        }
        else
        {
            Animator.SetBool(AnimWalk, false);
        }
       

        //Change direction
        Horizontal = Input.GetAxisRaw("Horizontal");
        //Move
        if (i_movement.x < 0.0f)
        {
            transform.localScale = new Vector3(-pSize, pSize, pSize);
        }
        else if (i_movement.x > 0.0f)
        {
            transform.localScale = new Vector3(pSize, pSize, pSize);
        }
    }

    // jump/dopublejump

    private void OnJump()
    {
        bool onTheGround = isOnGround();

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

    private void OnRecovery()
    {
        bool onTheGround = isOnGround();

        if (!recoveryKeyDown)
        {
            recoveryKeyDown = true;

            if (onTheGround || (canRecovery && EnableRecovery))
            {
                if (AnimRecoveryName != "")
                {
                    Animator.SetBool(AnimRecoveryName, true);
                }
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, this.RecoverySpeed);
            }

            if (!onTheGround)
            {
                canRecovery = false;
            }
        }
    }

    //Validate Ground
    private bool isOnGround()
    {
        float lengthToSearch = 0.1f;
        float colliderThreshhold = 0.1f;
        Vector2 linestart = new Vector2(this.transform.position.x, this.transform.position.y - this.GetComponent<Renderer>().bounds.extents.y - colliderThreshhold);
        Vector2 vectorToSearch = new Vector2(this.transform.position.x, linestart.y - lengthToSearch);
        RaycastHit2D hit = Physics2D.Linecast(linestart, vectorToSearch);
        return hit;
    }

    //Speed
    private void FixedUpdate()
    {
        if (dirX != 0 || isOnGround()){
            Rigidbody2D.velocity = new Vector2(dirX, Rigidbody2D.velocity.y);
        }
    }
}
