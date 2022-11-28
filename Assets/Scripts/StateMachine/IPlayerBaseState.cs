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
    public void Move(PlayerController player, Vector2 val, float speed);
    public void Jump(PlayerController player, float speed);
    public void OnNeutral(PlayerController player);
    public void OnCharged(PlayerController player);
    public void OnChargedCharged(PlayerController player);
    public void OnSpecial(PlayerController player);
    public void OnSpecialHold(PlayerController player);
    public void OnBang(PlayerController player);
    public void OnRecovery(PlayerController player);
    public void OnHit(PlayerController player);
    public void OnEnable(PlayerController player);
    public void OnDisable(PlayerController player);


    public bool hasGizmos();
    public gizmo? gz();
}

    public struct gizmo
    {
        public Color color;
        public Vector2 pos;
        public bool isSphere;
        public float radius;
        public Vector2 sz;
    }
