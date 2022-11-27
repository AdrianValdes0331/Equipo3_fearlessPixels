using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    [HideInInspector] public readonly Idle IdleState = new Idle();
    [HideInInspector] public readonly Walk WalkState = new Walk();
    [HideInInspector] public readonly Jump JumpState = new Jump();
    public NeutralAttack NeutralAState = new NeutralAttack();
    public ChargeAttack ChargeAState = new ChargeAttack();
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        TransitionToState(IdleState);
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

    public void OnMovement(InputValue val) {
        i_movement = val.Get<Vector2>();
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
