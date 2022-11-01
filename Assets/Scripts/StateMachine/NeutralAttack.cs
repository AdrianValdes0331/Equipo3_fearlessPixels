using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;

[System.Serializable]
public class NeutralAttack : Attack
{
    public override void EnterState(PlayerController player)
    {
        transform = player.transform;
        hitbox.transform = transform;
        MonoBehaviour.print("Entering Neutral");
        player.SetAnimatorTrigger(PlayerController.AnimStates.Neutral);
        hitbox.Start();
        hitbox.setResponder(this);
        hitbox.openCollissionCheck();

        g.sz = hitbox.sz;
        g.isSphere = hitbox.isSphere;
        g.radius = hitbox.radius;
        g.pos = hitbox.pos;

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
        g.color = hitbox.currColor;
    }
    public override void LateUpdate(PlayerController player) { }
    public override void Move(PlayerController player, InputValue val, float speed)
    {}
    public override void Jump(PlayerController player, float speed)
    {}
    public override void OnNeutral(PlayerController player)
    {}

}
