using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Walk : IPlayerBaseState
{
    Vector2 i_movement;
    float pSize;

    public void EnterState(PlayerController player)
    {
        pSize = System.Math.Abs(player.transform.localScale.x);
        MonoBehaviour.print("Entering walk");
        player.SetAnimatorTrigger(PlayerController.AnimStates.Walk);
    }

    public void OnCollisionEnter(PlayerController player, Collision2D col)
    {
       
    }

    public void OnTriggerEnter(PlayerController player, Collider2D col)
    {
        
    }

    public void Update(PlayerController player)
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

    public void LateUpdate(PlayerController player) 
    {
        //if (i_movement.x == 0) { player.TransitionToState(player.IdleState); }
        if (i_movement.x == 0) { player.TransitionToState(player.IdleState); }
    }

    public void Move(PlayerController player, InputValue val, float speed)
    {
        
    }

    public void Jump(PlayerController player, float speed)
    {
        player.rb.velocity = new Vector2(player.rb.velocity.x, speed);
        player.TransitionToState(player.JumpState);
    }

    public void OnNeutral(PlayerController player)
    {

    }
    public bool hasGizmos()
    {
        return false;
    }
    public gizmo? gz()
    {
        return null;
    }
}