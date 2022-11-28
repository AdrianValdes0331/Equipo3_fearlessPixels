using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : IPlayerBaseState
{
    bool done;
    float t;
    public void EnterState(PlayerController player)
    {
        done = false;
        t = player.stunTime;
        MonoBehaviour.print(player.name+": Entering Hitreact");
        player.SetAnimatorTrigger(PlayerController.AnimStates.Hitreact);
        player.rb.velocity = new Vector2(0, player.rb.velocity.y);
        player.rb.AddForce(player.hitForce);
        player.StartCoroutine(Active(player, t));
    }

    public void OnCollisionEnter(PlayerController player, Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Floor") && col.GetContact(0).normal.y >= 0.9)
        {
            player.stunTime = 0;
            player.hitForce = Vector2.zero;
            if (player.i_movement.x == 0) { player.TransitionToState(player.IdleState); }
            else { player.TransitionToState(player.WalkState); }
        }

    }

    public void OnTriggerEnter(PlayerController player, Collider2D col)
    {

    }

    public void Update(PlayerController player)
    {
        if (done) 
        { 
            player.stunTime = 0;
            player.hitForce = Vector2.zero;
            player.TransitionToState(player.JumpState);        
        }
    }

    public void LateUpdate(PlayerController player) { }

    public void Move(PlayerController player, Vector2 val, float speed)
    {
        player.TransitionToState(player.WalkState);
    }

    public void Jump(PlayerController player, float speed)
    {}

    public void OnNeutral(PlayerController player)
    {
        player.TransitionToState(player.NeutralAState);
    }
    public void OnCharged(PlayerController player)
    {
        player.TransitionToState(player.ChargeAState);
    }
    public void OnChargedCharged(PlayerController player)
    {
        player.TransitionToState(player.ChargeChargedState);
    }
    public void OnSpecial(PlayerController player)
    {
        player.TransitionToState(player.SpecialAState);
    }
    public void OnSpecialHold(PlayerController player)
    {}
    public void OnBang(PlayerController player)
    {}
    public void OnRecovery(PlayerController player)
    {}
    public void OnHit(PlayerController player)
    {
        player.TransitionToState(player.HitState);
    }
    public void OnEnable(PlayerController player)
    {}
    public void OnDisable(PlayerController player)
    {}
    public bool hasGizmos()
    {
        return false;
    }
    public gizmo? gz()
    {
        return null;
    }
    IEnumerator Active(PlayerController player, float t)
    {

        //uHitbox = false;
        yield return new WaitForSeconds(t);
        done = true;

    }
}
