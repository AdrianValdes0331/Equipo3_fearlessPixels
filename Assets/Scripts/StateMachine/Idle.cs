using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Idle : IPlayerBaseState
{
    public void EnterState(PlayerController player)
    {
        MonoBehaviour.print("Entering idle");
        player.SetAnimatorTrigger(PlayerController.AnimStates.Idle);
    }

    public void OnCollisionEnter(PlayerController player, Collision2D col)
    {

    }

    public void OnTriggerEnter(PlayerController player, Collider2D col)
    {

    }

    public void Update(PlayerController player)
    {
        if (player.rb.velocity.x != 0) { player.rb.velocity = new Vector2(0, player.rb.velocity.y); }
    }

    public void LateUpdate(PlayerController player) { }

    public void Move(PlayerController player, Vector2 val, float speed)
    {
        player.TransitionToState(player.WalkState);
    }

    public void Jump(PlayerController player, float speed)
    {
        player.rb.velocity = new Vector2(player.rb.velocity.x, speed);
        player.TransitionToState(player.JumpState);
    }

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
    {
        player.TransitionToState(player.BangAState);
    }
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

}