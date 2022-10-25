using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : IPlayerBaseState
{
    Vector2 i_movement;
    float pSize;
    bool doubleJump;

    public void EnterState(PlayerController player)
    {
        doubleJump = true;
        pSize = System.Math.Abs(player.transform.localScale.x);
        MonoBehaviour.print("Entering jump");
        player.SetAnimatorTrigger(PlayerController.AnimStates.Jump);
    }

    public void OnCollisionEnter(PlayerController player, Collision2D col)
    {
        Debug.Log(col.GetContact(0).normal.y);

        if (col.gameObject.layer == LayerMask.NameToLayer("Floor") && col.GetContact(0).normal.y >= 0.9)
        {
            if (player.rb.velocity.x == 0) { player.TransitionToState(player.IdleState); }
            else { player.TransitionToState(player.WalkState); }
        }
    }

    public void OnTriggerEnter(PlayerController player, Collider2D col)
    { }
    
    public void Update(PlayerController player)
    {
        if (i_movement.x < 0.0f)
        {
            player.transform.localScale = new Vector3(-pSize, pSize, pSize);
        }
        else if (i_movement.x > 0.0f)
        {
            player.transform.localScale = new Vector3(pSize, pSize, pSize);
        }
    }

    public void LateUpdate(PlayerController player) { }

    public void Move(PlayerController player, InputValue val, float speed)
    {
        i_movement = val.Get<Vector2>() * speed;
        player.rb.velocity = new Vector2(i_movement.x, player.rb.velocity.y);
    }

    void IPlayerBaseState.Jump(PlayerController player, float speed)
    {
        if (doubleJump)
        {
            player.rb.velocity = new Vector2(player.rb.velocity.x, speed);
            doubleJump = false;
        }
    }
}
