using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class NeutralAttack : Attack
{
    public override void EnterState(PlayerController player)
    {
        transform = player.transform;
        MonoBehaviour.print("Entering Neutral");
        player.SetAnimatorTrigger(PlayerController.AnimStates.Neutral);
        hitbox.setResponder(this);
    }

    public override void OnCollisionEffects()
    {}

    public override void OnCollisionEnter(PlayerController player, Collision2D col)
    {}

    public override void OnTriggerEnter(PlayerController player, Collider2D col)
    {}

    public override void Update(PlayerController player)
    {
        hitbox.hitboxUpdate();
    }
    public override void LateUpdate(PlayerController player) { }
    public override void Move(PlayerController player, InputValue val, float speed)
    {}
    public override void Jump(PlayerController player, float speed)
    {}

}
