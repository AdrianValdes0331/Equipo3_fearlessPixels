using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WalkMultiplayer : IMultiplayerBaseState
{
    Vector2 i_movement;
    float pSize;

    public void EnterState(MultiplayerControllerSM player)
    {
        pSize = System.Math.Abs(player.transform.localScale.x);
        MonoBehaviour.print("Entering walk");
        player.SetAnimatorTrigger(MultiplayerControllerSM.AnimStates.Walk);
    }

    public void OnCollisionEnter(MultiplayerControllerSM player, Collision2D col)
    {
       
    }

    public void OnTriggerEnter(MultiplayerControllerSM player, Collider2D col)
    {
        
    }

    public void Update(MultiplayerControllerSM player)
    {

        i_movement = player.i_movement;
        player.rb.velocity = new Vector2(i_movement.x*player.speed, player.rb.velocity.y);
        
        //Debug.Log(i_movement);

        if (player.rb.velocity.x < 0.0f)
        {
            player.transform.localScale = new Vector3(-pSize, pSize, pSize);
        }
        else if (player.rb.velocity.x > 0.0f)
        {
            player.transform.localScale = new Vector3(pSize, pSize, pSize);
        }
    }

    public void LateUpdate(MultiplayerControllerSM player) 
    {
        //if (i_movement.x == 0) { player.TransitionToState(player.IdleState); }
        if (i_movement.x == 0) { player.TransitionToState(player.IdleState); }
    }

    public void Move(MultiplayerControllerSM player, Vector2 val, float speed)
    {
        
    }

    public void Jump(MultiplayerControllerSM player, float speed)
    {
        player.rb.velocity = new Vector2(player.rb.velocity.x, speed);
        player.TransitionToState(player.JumpState);
    }

    public void OnNeutral(MultiplayerControllerSM player)
    {
        player.TransitionToState(player.NeutralAState);
    }
    public void OnCharged(MultiplayerControllerSM player)
    {
        player.rb.velocity = Vector3.zero;
        player.TransitionToState(player.ChargeAState);
    }
    public void OnChargedCharged(MultiplayerControllerSM player)
    {
        player.rb.velocity = Vector3.zero;
        player.TransitionToState(player.ChargeChargedState);
    }
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