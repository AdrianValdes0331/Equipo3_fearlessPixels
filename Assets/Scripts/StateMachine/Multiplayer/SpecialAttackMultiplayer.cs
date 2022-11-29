using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class SpecialAttackMultiplayer : AttackMultiplayer
{
    public SpecialMultiplayer special;
    public override void EnterState(MultiplayerControllerSM player)
    {
        MonoBehaviour.print("Entering Special");
        player.SetAnimatorTrigger(MultiplayerControllerSM.AnimStates.Special);
        special.SpecialStart(player);
    }

    public override void OnCollisionEffects()
    {
        
    }

    public override void OnCollisionEnter(MultiplayerControllerSM player, Collision2D col)
    {
        
    }

    public override void OnTriggerEnter(MultiplayerControllerSM player, Collider2D col)
    {
        
    }

    public override void Update(MultiplayerControllerSM player)
    {
        special.SpecialUpdate(player);
    }
    public override void LateUpdate(MultiplayerControllerSM player) { }
    public override void Move(MultiplayerControllerSM player, Vector2 val, float speed)
    {}
    public override void Jump(MultiplayerControllerSM player, float speed)
    {}
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
    {

    }
    public override void OnDisable(MultiplayerControllerSM player)
    {}

}
