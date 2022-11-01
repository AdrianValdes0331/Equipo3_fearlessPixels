using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public abstract class Attack : IPlayerBaseState, IHitboxResponder
{
    
    [SerializeField] private float dmg;
    [SerializeField] protected Hitbox hitbox;
    [SerializeField] private int force;
    [SerializeField] private int angle;
    [SerializeField] protected Transform transform;

    public abstract void EnterState(PlayerController player);
    public abstract void Update(PlayerController player);
    public abstract void LateUpdate(PlayerController player);
    public abstract void OnTriggerEnter(PlayerController player, Collider2D col);
    public abstract void OnCollisionEnter(PlayerController player, Collision2D col);
    public abstract void Move(PlayerController player, InputValue val, float speed);
    public abstract void Jump(PlayerController player, float speed);
    public abstract void OnCollisionEffects(); //Things to do after an attack hits dependent on implementation
    //All attacks have the same implementation for CollisionedWith
    public void CollisionedWith(Collider2D collider){
        OnCollisionEffects();
        //if the collider's grandparent is the parent of the attack's gameObject
        //return without doing anything
        if(collider.transform.parent.transform.parent == transform.parent) { return; } 
        Hurtbox hurtbox = collider.GetComponent<Hurtbox>();
        //if the collider has a hurtbox
        if (hurtbox != null)
        {
            BangLvl bang = transform.parent.GetComponent<BangLvl>();
            bang.bangUpdate(dmg, true);
            Debug.Log("Hit player");
            hurtbox.getHitBy(dmg, force, angle, transform.position.x);
        }
    }

}
