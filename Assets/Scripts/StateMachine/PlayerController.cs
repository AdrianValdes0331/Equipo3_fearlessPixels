using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public Vector2 i_movement;
    public float speed;
    public float jspeed;
    public Animator animator;
    [HideInInspector] public Rigidbody2D rb;
    private IPlayerBaseState currState;
    private bool draw = false;
    gizmo g;
    PlayerControls playerControls;
    PlayerControls.PlayerActions playerAct;

    [HideInInspector] public readonly Idle IdleState = new Idle();
    [HideInInspector] public readonly Walk WalkState = new Walk();
    [HideInInspector] public readonly Jump JumpState = new Jump();
    public NeutralAttack NeutralAState = new NeutralAttack();
    public ChargeAttack ChargeAState = new ChargeAttack();
    public ChargeAttackCharged ChargeChargedState = new ChargeAttackCharged();
    // Start is called before the first frame update
    void Awake()
    {
        playerControls = new PlayerControls();
        playerAct = playerControls.Player;

        playerAct.Movement.performed += ctx => OnMovement(playerAct.Movement.ReadValue<Vector2>());
        playerAct.Movement.canceled += ctx => OnMovement(Vector2.zero);
        //playerAct.Bang.performed += OnBang;
        playerAct.Jump.performed += ctx => OnJump();
        //playerAct.Recovery.performed += ctx => OnRecovery();
        //playerAct.Special.performed += ctx => OnSpecial();
        playerAct.StrongKick.started += 
            ctx =>
            {
                if (ctx.interaction is SlowTapInteraction)
                {
                    OnCharged();
                }
            };
        playerAct.StrongKick.performed += 
            ctx =>
            {
                if(ctx.interaction is SlowTapInteraction)
                {
                    OnCharged();
                }
                else
                {
                    Debug.Log("Tapped");
                    OnStrongKick();
                }
            };
        playerAct.StrongKick.canceled +=
            ctx =>
            {
                if (ctx.interaction is SlowTapInteraction)
                {
                    TransitionToState(IdleState);
                };
            };
        //playerAct.Stron
        playerAct.WeakAttack.performed += ctx => OnWeakAttack();

        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        TransitionToState(IdleState);
    }

    void OnEnable()
    {
        playerAct.Enable();
    }

    void OnDisable()
    {
        playerAct.Disable();
    }

    // Update is called once per frame
    void Update()
    {
       currState.Update(this);
       if(currState.gz() != null)
        {
            g = (gizmo) currState.gz();
        }
    }

    private void LateUpdate()
    {
        currState.LateUpdate(this);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        currState.OnTriggerEnter(this, other);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        currState.OnCollisionEnter(this, collision);
    }

    public void OnMovement(Vector2 val) {
        i_movement = val;
        Debug.Log(i_movement);
        currState.Move(this, val, speed);
    }

    public void OnJump()
    {
        currState.Jump(this, jspeed);
    }

    public void OnWeakAttack(){
        currState.OnNeutral(this);
    }
    
    public void OnStrongKick()
    {
        currState.OnCharged(this);
    }

    public void OnCharged()
    {
        currState.OnChargedCharged(this);
    }

    internal void TransitionToState(IPlayerBaseState state) {
        currState = state;
        draw = currState.hasGizmos();
        currState.EnterState(this);
    }

    public enum AnimStates {
        Idle = 0,
        Walk = 1,
        Jump = 2,
        Neutral = 3,
        Charge = 4,
        Special = 5,
        Bang = 6,
        Hitreact =7
    }

    public void SetAnimatorTrigger(AnimStates state) {
        animator.SetInteger("anim", (int)state);
    }

    private void OnDrawGizmos(){
        if(draw){
            Gizmos.color = g.color;
            Gizmos.matrix = Matrix4x4.TRS(g.pos, transform.rotation, transform.localScale);
            if (!g.isSphere)
            {
                Gizmos.DrawCube(Vector3.zero, new Vector3(g.sz.x, g.sz.y, 0)); // Because size is halfExtents
            }
            else
            {
                Gizmos.DrawSphere(Vector3.zero, g.radius);
            }
        }
    }

}
