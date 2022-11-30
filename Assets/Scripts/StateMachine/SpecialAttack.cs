using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class SpecialAttack : Attack
{
    public Special special;
    bool done;
    public override void EnterState(PlayerController player)
    {
        done = false;
        MonoBehaviour.print("Entering Special");
        player.SetAnimatorTrigger(PlayerController.AnimStates.Special);
        special.SpecialStart(player);
        if (activeTime > 0)
        {
            player.StartCoroutine(Active(player, activeTime));
        }
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
        if (done)
        {
            if (player.i_movement.x == 0)
            {
                player.TransitionToState(player.IdleState);
            }
            else
            {
                player.TransitionToState(player.WalkState);
            }
        }
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
    {
        player.TransitionToState(player.HitState);
    }
    public override void OnEnable(PlayerController player)
    {

    }
    public override void OnDisable(PlayerController player)
    {}

    IEnumerator Active(PlayerController player, float t)
    {

        //uHitbox = false;
        yield return new WaitForSeconds(t);
        hitbox.closeCollissionCheck();
        done = true;

    }

}
