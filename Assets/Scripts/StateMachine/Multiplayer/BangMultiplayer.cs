using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class BangMultiplayer : AttackMultiplayer
{
    public BangAttackMultiplayer bangAttack;

    public override void EnterState(MultiplayerControllerSM player)
    {
        BangLvlMultiplayer bang = player.gameObject.GetComponent<BangLvlMultiplayer>();
        if (bang.tryBang())
        {
            MonoBehaviour.print("Entering Bang");
            player.SetAnimatorTrigger(MultiplayerControllerSM.AnimStates.Bang);
            bangAttack.BangStart(player);
        }
        else
        {
            if (player.i_movement.x == 0) { player.TransitionToState(player.IdleState); }
            else { player.TransitionToState(player.WalkState); }

        }
    }

    public override void OnCollisionEffects()
    { }

    public override void OnCollisionEnter(MultiplayerControllerSM player, Collision2D col)
    { }

    public override void OnTriggerEnter(MultiplayerControllerSM player, Collider2D col)
    { }

    public override void Update(MultiplayerControllerSM player)
    {
        bangAttack.BangUpdate(player);
    }
    public override void LateUpdate(MultiplayerControllerSM player)
    { }
    public override void Move(MultiplayerControllerSM player, Vector2 val, float speed)
    { }
    public override void Jump(MultiplayerControllerSM player, float speed)
    { }
    public override void OnNeutral(MultiplayerControllerSM player)
    { }
    public override void OnCharged(MultiplayerControllerSM player)
    { }
    public override void OnChargedCharged(MultiplayerControllerSM player)
    { }
    public override void OnSpecial(MultiplayerControllerSM player)
    { }
    public override void OnSpecialHold(MultiplayerControllerSM player)
    { }
    public override void OnBang(MultiplayerControllerSM player)
    { }
    public override void OnRecovery(MultiplayerControllerSM player)
    { }
    public override void OnHit(MultiplayerControllerSM player)
    { }
    public override void OnEnable(MultiplayerControllerSM player)
    { }
    public override void OnDisable(MultiplayerControllerSM player)
    { }

}
