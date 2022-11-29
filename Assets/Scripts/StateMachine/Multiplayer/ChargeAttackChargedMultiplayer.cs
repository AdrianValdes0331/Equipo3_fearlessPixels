using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

[System.Serializable]
public class ChargeAttackChargedMultiplayer : AttackMultiplayer
{
    //bool uHitbox = false;
    bool done;
    Vector2 i_movement;
    float pSize;
    float startTime;
    float dT;
    float dmgScale = 0.8f;
    float tScale = 0.2f;
    float flashInt;
    Sequence anim;
    SpriteRenderer sr;
    Color c = new Color(1.0f, 1.0f, 0.57f, 1);
    Color start = new Color(1, 1, 1, 1);

    public override void EnterState(MultiplayerControllerSM player)
    {
        pSize = System.Math.Abs(player.transform.localScale.x);
        done = false;
        transform = player.transform;
        hitbox.transform = transform;
        MonoBehaviour.print("Charging!");
        player.SetAnimatorTrigger(MultiplayerControllerSM.AnimStates.Idle);

        startTime = Time.fixedTime;

        sr = player.transform.GetChild(0).GetComponent<SpriteRenderer>();
        anim = DOTween.Sequence();
        flashInt = 0.3f;
        anim.Append(sr.DOColor(c, flashInt));
        anim.Append(sr.DOColor(start, flashInt));
        anim.SetLoops(-1, LoopType.Restart);

    }

    public override void OnCollisionEffects()
    {}

    public override void OnCollisionEnter(MultiplayerControllerSM player, Collision2D col)
    {}

    public override void OnTriggerEnter(MultiplayerControllerSM player, Collider2D col)
    {}

    public override void Update(MultiplayerControllerSM player)
    {
        dT = Time.fixedTime - startTime;
        dT = Mathf.Clamp(dT, 0.0f, 2.5f);

        if (dT >= 2.5f && !done)
        {
            anim.Kill(true);
            sr.color = c;
        }

        multiplier = 1 + dmgScale * dT;

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
            if (i_movement.x == 0)
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
    {
        anim.Kill(true);
        sr.color = start;
        player.TransitionToState(player.ChargeAState);
    }
    public override void OnChargedCharged(MultiplayerControllerSM player)
    {
        anim.Kill(true);
        Debug.Log("Charged!");
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
        sr.DOColor(start, 0.2f);

    }

}
