using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class MultiplayerControllerSM : MonoBehaviour
{
    PlayerInput playerInput;
    public NHurtboxMultiplayer hb;
    [HideInInspector] public Vector2 i_movement;
    [HideInInspector] public float stunTime;
    [HideInInspector] public Vector2 hitForce;
    public float speed;
    public float jspeed;
    public Animator animator;
    [HideInInspector] public Rigidbody2D rb;
    private IMultiplayerBaseState currState;
    private bool draw = false;
    gizmo g;
    PlayerControls playerControls;
    PlayerControls.PlayerActions playerAct;

    [HideInInspector] public readonly IdleMultiplayer IdleState = new IdleMultiplayer();
    [HideInInspector] public readonly WalkMultiplayer WalkState = new WalkMultiplayer();
    [HideInInspector] public readonly JumpMultiplayer JumpState = new JumpMultiplayer();
    [HideInInspector] public readonly HitMultiplayer HitState = new HitMultiplayer();
    public NeutralAttackMultiplayer NeutralAState = new NeutralAttackMultiplayer();
    public ChargeAttackMultiplayer ChargeAState = new ChargeAttackMultiplayer();
    public ChargeAttackChargedMultiplayer ChargeChargedState = new ChargeAttackChargedMultiplayer();
    public SpecialAttackMultiplayer SpecialAState = new SpecialAttackMultiplayer();
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        // playerControls = new PlayerControls();
        // playerAct = playerControls.Player;

        playerInput.actions["Movement"].performed += ctx => OnMovement(playerInput.actions["Movement"].ReadValue<Vector2>());
        playerInput.actions["Movement"].canceled += ctx => OnMovement(Vector2.zero);
        //playerAct.Bang.performed += OnBang;
        playerInput.actions["Jump"].performed += ctx => OnJump();
        //playerAct.Recovery.performed += ctx => OnRecovery();
        playerInput.actions["Special"].performed += ctx => OnSpecial();
        playerInput.actions["StrongKick"].started +=
            ctx =>
            {
                if (ctx.interaction is SlowTapInteraction)
                {
                    OnCharged();
                }
            };
        playerInput.actions["StrongKick"].performed +=
            ctx =>
            {
                if (ctx.interaction is SlowTapInteraction)
                {
                    OnCharged();
                }
                else
                {
                    Debug.Log("Tapped");
                    OnStrongKick();
                }
            };
        playerInput.actions["StrongKick"].canceled +=
            ctx =>
            {
                if (ctx.interaction is SlowTapInteraction)
                {
                    Debug.Log("Charge Canceled!");
                    OnStrongKick();
                };
            };
        //playerAct.Stron
        playerInput.actions["WeakAttack"].performed += ctx => OnWeakAttack();

        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        TransitionToState(IdleState);
    }

    void OnEnable()
    {
        //playerAct.Enable();
        hb.HitReact += OnHit;
    }

    void OnDisable()
    {
        //playerAct.Disable();
        hb.HitReact -= OnHit;
    }

    // Update is called once per frame
    void Update()
    {
        currState.Update(this);
        if (currState.gz() != null)
        {
            g = (gizmo)currState.gz();
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

    public void OnMovement(Vector2 val)
    {
        i_movement = val;
        Debug.Log(i_movement);
        currState.Move(this, val, speed);
    }

    public void OnJump()
    {
        currState.Jump(this, jspeed);
    }

    public void OnWeakAttack()
    {
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

    public void OnSpecial()
    {
        currState.OnSpecial(this);
    }

    public void OnHit(Vector2 force)
    {
        hitForce = force;
        if (force.magnitude == 0)
        {
            stunTime = 0;
            currState.OnHit(this);
        }
        else
        {
            stunTime = 1f + (force.magnitude - 59) * (1.5f / (8000 - 59));
            stunTime = Mathf.Clamp(stunTime, 1f, 2.5f);
            Debug.Log("stun: " + stunTime);
            currState.OnHit(this);
        }
    }

    internal void TransitionToState(IMultiplayerBaseState state)
    {
        currState = state;
        draw = currState.hasGizmos();
        currState.EnterState(this);
    }

    public enum AnimStates
    {
        Idle = 0,
        Walk = 1,
        Jump = 2,
        Neutral = 3,
        Charge = 4,
        Special = 5,
        Bang = 6,
        Hitreact = 7,
        Recovery = 8
    }

    public void SetAnimatorTrigger(AnimStates state)
    {
        animator.SetInteger("anim", (int)state);
    }

    private void OnDrawGizmos()
    {
        if (draw)
        {
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
