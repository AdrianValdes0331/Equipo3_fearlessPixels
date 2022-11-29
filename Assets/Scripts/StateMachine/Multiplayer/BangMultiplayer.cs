using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BangMultiplayer : AttackMultiplayer
{
    public override void EnterState(MultiplayerControllerSM player)
    {
        MonoBehaviour.print("Entering idle");
        player.SetAnimatorTrigger(MultiplayerControllerSM.AnimStates.Idle);
    }

    public override void OnCollisionEffects()
    {
        throw new System.NotImplementedException();
    }

    public override void OnCollisionEnter(MultiplayerControllerSM player, Collision2D col)
    {
        throw new System.NotImplementedException();
    }

    public override void OnTriggerEnter(MultiplayerControllerSM player, Collider2D col)
    {
        throw new System.NotImplementedException();
    }

    public override void Update(MultiplayerControllerSM player)
    {
        throw new System.NotImplementedException();
    }
    public override void LateUpdate(MultiplayerControllerSM player) { }
    public override void Move(MultiplayerControllerSM player, Vector2 val, float speed)
    {
        throw new System.NotImplementedException();
    }
    public override void Jump(MultiplayerControllerSM player, float speed)
    {
        throw new System.NotImplementedException();
    }
    public override void OnNeutral(MultiplayerControllerSM player)
    {}
    public override void OnCharged(MultiplayerControllerSM player)
    {}
    public override void OnChargedCharged(MultiplayerControllerSM player)
    {}
    public override void OnSpecial(MultiplayerControllerSM player)
    {}
    public override void OnSpecialHold(MultiplayerControllerSM player)
    {}
    public override void OnBang(MultiplayerControllerSM player)
    {}
    public override void OnRecovery(MultiplayerControllerSM player)
    {}
    public override void OnHit(MultiplayerControllerSM player)
    {
        player.TransitionToState(player.HitState);
    }
    public override void OnEnable(MultiplayerControllerSM player)
    {}
    public override void OnDisable(MultiplayerControllerSM player)
    {}

}
