using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class ChargeAttack : Attack
{
    //bool uHitbox = false;
    bool done;
    Vector2 i_movement;
    float pSize;

    public override void EnterState(PlayerController player)
    {
        pSize = System.Math.Abs(player.transform.localScale.x);
        done = false;
        transform = player.transform;
        hitbox.transform = transform;
        MonoBehaviour.print("Entering TapCharge");
        player.SetAnimatorTrigger(PlayerController.AnimStates.Charge);
        hitbox.Start();
        hitbox.setResponder(this);
        hitbox.openCollissionCheck();

        g.sz = hitbox.sz;
        g.isSphere = hitbox.isSphere;
        g.radius = hitbox.radius;
        g.pos = hitbox.pos;
        player.StartCoroutine(Active(player, activeTime));
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
        g.sz = hitbox.sz;
        g.isSphere = hitbox.isSphere;
        g.radius = hitbox.radius;
        g.pos = hitbox.pos;
        g.color = hitbox.currColor;

        i_movement = player.i_movement;

        if (i_movement.x < 0.0f)
        {
            player.transform.localScale = new Vector3(-pSize, pSize, pSize);
        }
        else if (i_movement.x > 0.0f)
        {
            player.transform.localScale = new Vector3(pSize, pSize, pSize);
        }

        if (done)
        {
            if(i_movement.x == 0)
            {
                player.TransitionToState(player.IdleState);
            }
            else
            {
                player.TransitionToState(player.WalkState);
            }
        }
    }
    public override void LateUpdate(PlayerController player) 
    {}
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
    {}
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
