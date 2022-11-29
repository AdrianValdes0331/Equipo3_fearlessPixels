using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpMultiplayer : IMultiplayerBaseState
{
    Vector2 i_movement;
    float pSize;
    bool doubleJump;

    public void EnterState(MultiplayerControllerSM player)
    {
        doubleJump = true;
        pSize = System.Math.Abs(player.transform.localScale.x);
        MonoBehaviour.print(player.name+": Entering jump");
        player.SetAnimatorTrigger(MultiplayerControllerSM.AnimStates.Jump);
    }

    public void OnCollisionEnter(MultiplayerControllerSM player, Collision2D col)
    {
        Debug.Log(col.GetContact(0).normal.y);

        if (col.gameObject.layer == LayerMask.NameToLayer("Floor") && col.GetContact(0).normal.y >= 0.8)
        {
            if (player.rb.velocity.x == 0) { player.TransitionToState(player.IdleState); }
            else { player.TransitionToState(player.WalkState); }
        }
    }

    public void OnTriggerEnter(MultiplayerControllerSM player, Collider2D col)
    { }
    
    public void Update(MultiplayerControllerSM player)
    {

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
    }

    public void LateUpdate(MultiplayerControllerSM player) { }

    public void Move(MultiplayerControllerSM player, Vector2 val, float speed)
    {
        
    }

    void IMultiplayerBaseState.Jump(MultiplayerControllerSM player, float speed)
    {
        if (doubleJump)
        {
            player.rb.velocity = new Vector2(player.rb.velocity.x, speed);
            doubleJump = false;
        }
    }

    public void OnNeutral(MultiplayerControllerSM player)
    {

    }
    public void OnCharged(MultiplayerControllerSM player)
    {}
    public void OnChargedCharged(MultiplayerControllerSM player)
    {}
    public void OnSpecial(MultiplayerControllerSM player)
    {}
    public void OnSpecialHold(MultiplayerControllerSM player)
    {}
    public void OnBang(MultiplayerControllerSM player)
    {}
    public void OnRecovery(MultiplayerControllerSM player)
    {}
    public void OnHit(MultiplayerControllerSM player)
    {
        player.TransitionToState(player.HitState);
    }
    public void OnEnable(MultiplayerControllerSM player)
    {}
    public void OnDisable(MultiplayerControllerSM player)
    {}
    public bool hasGizmos()
    {
        return false;
    }
    public gizmo? gz()
    {
        return null;
    }
}
