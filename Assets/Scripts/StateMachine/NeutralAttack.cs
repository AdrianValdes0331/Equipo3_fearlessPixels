using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NeutralAttack : Attack
{
    public override void EnterState(PlayerController player)
    {
        MonoBehaviour.print("Entering idle");
        player.SetAnimatorTrigger(PlayerController.AnimStates.Idle);
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
    public override void Move(PlayerController player, InputValue val, float speed)
    {}
    public override void Jump(PlayerController player, float speed)
    {}

}
