using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IPlayerBaseState
{
    public void EnterState(PlayerController player);
    public void Update(PlayerController player);
    public void LateUpdate(PlayerController player);
    public void OnTriggerEnter(PlayerController player, Collider2D col);
    public void OnCollisionEnter(PlayerController player, Collision2D col);
    public void Move(PlayerController player, InputValue val, float speed);
    public void Jump(PlayerController player, float speed);
}
