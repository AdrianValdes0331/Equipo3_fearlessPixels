using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitMultiplayer : IMultiplayerBaseState
{
    bool done;
    float t;
    public void EnterState(MultiplayerControllerSM player)
    {
        done = false;
        t = player.stunTime;
        MonoBehaviour.print(player.name+": Entering Hitreact");
        player.SetAnimatorTrigger(MultiplayerControllerSM.AnimStates.Hitreact);
        player.rb.velocity = new Vector2(0, player.rb.velocity.y);
        player.rb.AddForce(player.hitForce);
        player.StartCoroutine(Active(player, t));
    }

    public void OnCollisionEnter(MultiplayerControllerSM player, Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Floor") && col.GetContact(0).normal.y >= 0.9)
        {
            player.stunTime = 0;
            player.hitForce = Vector2.zero;
            if (player.i_movement.x == 0) { player.TransitionToState(player.IdleState); }
            else { player.TransitionToState(player.WalkState); }
        }

    }

    public void OnTriggerEnter(MultiplayerControllerSM player, Collider2D col)
    {

    }

    public void Update(MultiplayerControllerSM player)
    {
        if (done) 
        { 
            player.stunTime = 0;
            player.hitForce = Vector2.zero;
            player.TransitionToState(player.JumpState);        
        }
    }

    public void LateUpdate(MultiplayerControllerSM player) { }

    public void Move(MultiplayerControllerSM player, Vector2 val, float speed)
    {
        player.TransitionToState(player.WalkState);
    }

    public void Jump(MultiplayerControllerSM player, float speed)
    {}

    public void OnNeutral(MultiplayerControllerSM player)
    {
        player.TransitionToState(player.NeutralAState);
    }
    public void OnCharged(MultiplayerControllerSM player)
    {
        player.TransitionToState(player.ChargeAState);
    }
    public void OnChargedCharged(MultiplayerControllerSM player)
    {
        player.TransitionToState(player.ChargeChargedState);
    }
    public void OnSpecial(MultiplayerControllerSM player)
    {
        player.TransitionToState(player.SpecialAState);
    }
    public void OnSpecialHold(MultiplayerControllerSM player)
    {}
    public void OnBang(MultiplayerControllerSM player)
    {}
    public void OnRecovery(MultiplayerControllerSM player)
    {}
    public void OnHit(MultiplayerControllerSM player)
    {
        player.TransitionToState(player.HitState);
    }
    public void OnEnable(MultiplayerControllerSM player)
    {}
    public void OnDisable(MultiplayerControllerSM player)
    {}
    public bool hasGizmos()
    {
        return false;
    }
    public gizmo? gz()
    {
        return null;
    }
    IEnumerator Active(MultiplayerControllerSM player, float t)
    {

        //uHitbox = false;
        yield return new WaitForSeconds(t);
        done = true;

    }
}
