using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IMultiplayerBaseState
{
    public void EnterState(MultiplayerControllerSM player);
    public void Update(MultiplayerControllerSM player);
    public void LateUpdate(MultiplayerControllerSM player);
    public void OnTriggerEnter(MultiplayerControllerSM player, Collider2D col);
    public void OnCollisionEnter(MultiplayerControllerSM player, Collision2D col);
    public void Move(MultiplayerControllerSM player, Vector2 val, float speed);
    public void Jump(MultiplayerControllerSM player, float speed);
    public void OnNeutral(MultiplayerControllerSM player);
    public void OnCharged(MultiplayerControllerSM player);
    public void OnChargedCharged(MultiplayerControllerSM player);
    public void OnSpecial(MultiplayerControllerSM player);
    public void OnSpecialHold(MultiplayerControllerSM player);
    public void OnBang(MultiplayerControllerSM player);
    public void OnRecovery(MultiplayerControllerSM player);
    public void OnHit(MultiplayerControllerSM player);
    public void OnEnable(MultiplayerControllerSM player);
    public void OnDisable(MultiplayerControllerSM player);


    public bool hasGizmos();
    public gizmo? gz();
}
