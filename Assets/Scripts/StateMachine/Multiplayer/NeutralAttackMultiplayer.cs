using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;

[System.Serializable]
public class NeutralAttackMultiplayer : AttackMultiplayer
{
    //bool uHitbox = false;
    bool queued;
    bool done;
    Vector2 i_movement;
    float pSize;

    public override void EnterState(MultiplayerControllerSM player)
    {
        pSize = System.Math.Abs(player.transform.localScale.x);
        queued = false;
        done = false;
        transform = player.transform;
        hitbox.transform = transform;
        MonoBehaviour.print("Entering Neutral");
        player.SetAnimatorTrigger(MultiplayerControllerSM.AnimStates.Neutral);
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
        
        player.rb.velocity = new Vector2(i_movement.x*player.speed, player.rb.velocity.y);

        if (i_movement.x < 0.0f)
        {
            player.transform.localScale = new Vector3(-pSize, pSize, pSize);
        }
        else if (i_movement.x > 0.0f)
        {
            player.transform.localScale = new Vector3(pSize, pSize, pSize);
        }

        if(done)
        {
            if(queued)
            {
                player.TransitionToState(player.NeutralAState);
            }
            else if(player.rb.velocity.x == 0)
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
    {

        hitbox.closeCollissionCheck();
        player.rb.velocity = new Vector2(player.rb.velocity.x, speed);
        player.TransitionToState(player.JumpState);

    }
    public override void OnNeutral(MultiplayerControllerSM player)
    {

        queued = true;

    }
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
