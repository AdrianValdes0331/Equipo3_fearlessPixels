using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Idle : IPlayerBaseState
{
    public void EnterState(PlayerController player)
    {
        MonoBehaviour.print("Entering idle");
        player.SetAnimatorTrigger(PlayerController.AnimStates.Idle);
    }

    public void OnCollisionEnter(PlayerController player, Collision2D col)
    {
        
    }

    public void OnTriggerEnter(PlayerController player, Collider2D col)
    {
        
    }

    public void Update(PlayerController player)
    {
        if (player.rb.velocity.x != 0) { player.rb.velocity = new Vector2(0, player.rb.velocity.y); }
    }

    public void LateUpdate(PlayerController player) { }

    public void Move(PlayerController player, InputValue val, float speed)
    {
        player.TransitionToState(player.WalkState);
    }

    public void Jump(PlayerController player, float speed)
    {
        player.rb.velocity = new Vector2(player.rb.velocity.x, speed);
        player.TransitionToState(player.JumpState);
    }

}