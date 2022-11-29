using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bang : Attack
{
    public BangAttack bangAttack;
    public override void EnterState(PlayerController player)
    {
        MonoBehaviour.print("Entering Bang");
        player.SetAnimatorTrigger(PlayerController.AnimStates.Bang);
        bangAttack.BangStart(player);    
    }

    public override void OnCollisionEffects()
    {
        throw new System.NotImplementedException();
    }

    public override void OnCollisionEnter(PlayerController player, Collision2D col)
    {
        throw new System.NotImplementedException();
    }

    public override void OnTriggerEnter(PlayerController player, Collider2D col)
    {
        throw new System.NotImplementedException();
    }

    public override void Update(PlayerController player)
    {
        throw new System.NotImplementedException();
    }
    public override void LateUpdate(PlayerController player) { }
    public override void Move(PlayerController player, Vector2 val, float speed)
    {
        throw new System.NotImplementedException();
    }
    public override void Jump(PlayerController player, float speed)
    {
        throw new System.NotImplementedException();
    }
    public override void OnNeutral(PlayerController player)
    {}
    public override void OnCharged(PlayerController player)
    {}
    public override void OnChargedCharged(PlayerController player)
    {}
    public override void OnSpecial(PlayerController player)
    {}
    public override void OnSpecialHold(PlayerController player)
    {}
    public override void OnBang(PlayerController player)
    {}
    public override void OnRecovery(PlayerController player)
    {}
    public override void OnHit(PlayerController player)
    {
        player.TransitionToState(player.HitState);
    }
    public override void OnEnable(PlayerController player)
    {}
    public override void OnDisable(PlayerController player)
    {}

}
