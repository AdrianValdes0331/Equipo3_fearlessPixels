using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class ChargeAttackMultiplayer : AttackMultiplayer
{
    //bool uHitbox = false;
    bool done;
    Vector2 i_movement;
    float pSize;

    public override void EnterState(MultiplayerControllerSM player)
    {
        pSize = System.Math.Abs(player.transform.localScale.x);
        done = false;
        transform = player.transform;
        hitbox.transform = transform;
        MonoBehaviour.print("Entering TapCharge");
        player.SetAnimatorTrigger(MultiplayerControllerSM.AnimStates.Charge);
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

    public override void OnCollisionEnter(MultiplayerControllerSM player, Collision2D col)
    {}

    public override void OnTriggerEnter(MultiplayerControllerSM player, Collider2D col)
    {}

    public override void Update(MultiplayerControllerSM player)
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
    public override void LateUpdate(MultiplayerControllerSM player) 
    {}
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
    {}
    public override void OnDisable(MultiplayerControllerSM player)
    {}
    IEnumerator Active(MultiplayerControllerSM player, float t)
    {

        //uHitbox = false;
        yield return new WaitForSeconds(t);
        hitbox.closeCollissionCheck();
        done = true;

    }

}
