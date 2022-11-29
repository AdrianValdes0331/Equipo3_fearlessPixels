using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IdleMultiplayer : IMultiplayerBaseState
{
    public void EnterState(MultiplayerControllerSM player)
    {
        MonoBehaviour.print("Entering idle");
        player.SetAnimatorTrigger(MultiplayerControllerSM.AnimStates.Idle);
    }

    public void OnCollisionEnter(MultiplayerControllerSM player, Collision2D col)
    {

    }

    public void OnTriggerEnter(MultiplayerControllerSM player, Collider2D col)
    {

    }

    public void Update(MultiplayerControllerSM player)
    {
        if (player.rb.velocity.x != 0) { player.rb.velocity = new Vector2(0, player.rb.velocity.y); }
    }

    public void LateUpdate(MultiplayerControllerSM player) { }

    public void Move(MultiplayerControllerSM player, Vector2 val, float speed)
    {
        player.TransitionToState(player.WalkState);
    }

    public void Jump(MultiplayerControllerSM player, float speed)
    {
        player.rb.velocity = new Vector2(player.rb.velocity.x, speed);
        player.TransitionToState(player.JumpState);
    }

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

}