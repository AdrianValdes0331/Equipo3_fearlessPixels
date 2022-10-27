using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Vector2 i_movement;
    public float speed;
    public float jspeed;
    public Animator animator;
    [HideInInspector] public Rigidbody2D rb;
    private IPlayerBaseState currState;

    [HideInInspector] public readonly Idle IdleState = new Idle();
    [HideInInspector] public readonly Walk WalkState = new Walk();
    [HideInInspector] public readonly Jump JumpState = new Jump();
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

    internal void TransitionToState(IPlayerBaseState state) {
        currState = state;
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

}
