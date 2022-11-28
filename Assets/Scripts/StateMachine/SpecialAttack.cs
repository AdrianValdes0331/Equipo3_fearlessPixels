using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class SpecialAttack : Attack
{
    public Special special;
    public override void EnterState(PlayerController player)
    {
        MonoBehaviour.print("Entering Special");
        player.SetAnimatorTrigger(PlayerController.AnimStates.Special);
        special.SpecialStart(player);
    }

    public override void OnCollisionEffects()
    {
        
    }

    public override void OnCollisionEnter(PlayerController player, Collision2D col)
    {
        
    }

    public override void OnTriggerEnter(PlayerController player, Collider2D col)
    {
        
    }

    public override void Update(PlayerController player)
    {
        special.SpecialUpdate(player);
    }
    public override void LateUpdate(PlayerController player) { }
    public override void Move(PlayerController player, Vector2 val, float speed)
    {}
    public override void Jump(PlayerController player, float speed)
    {}
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
    {}
    public override void OnEnable(PlayerController player)
    {

    }
    public override void OnDisable(PlayerController player)
    {}

}
