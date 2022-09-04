using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System;

public class Controller : MonoBehaviour
{
    @P1Controls controls;

    private float dirX;
    public Animator Animator;
    private Rigidbody2D Rigidbody2D;
    public float MaxSpeed;
    public float JumpSpeed;
    public GameObject Scope;
    public GameObject Misile;
    public Transform FirePoint;
    public Transform ScopeSpawn;

    public bool EnableDoubleJump = true;

    bool canDoubleJump = true;
    bool jumpKeyDown = false;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        MaxSpeed = 5.0f;
    }

    void Awake()
    {
        controls = new @P1Controls();

        controls.Gameplay.Jump.performed += ctx => JumpController();
        controls.Gameplay.Chinsil.performed += ctx => Chinchisil();
    }

    //P1 Controller
    private void JumpController()
    {
        bool onTheGround = isOnGround();
        if (onTheGround)
        {
            canDoubleJump = true;
        }       
        jumpKeyDown = false;      
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

    // Update is called once per frame
    void Update()
    {
        //Empty    
    }

    private void Chinchisil()
    {
        //Misil
        if (!Animator.GetCurrentAnimatorStateInfo(0).IsName("Chinmisil") && !GameObject.FindWithTag("scope"))
        {          
            Animator.SetBool("Walk", false);
            Animator.SetBool("Chinmisil", true);
            Shoot();
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

    //Controls
    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}
