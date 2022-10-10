using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : PlayerBaseState, IHitboxResponder
{
    private float dmg;
    private Hitbox hitbox;
    private int force;
    private int angle;
    private Transform transform;

    public abstract void EnterState(PlayerController player);
    public abstract void Update(PlayerController player);
    public abstract void OnTriggerEnter(PlayerController player, Collider2D col);
    public abstract void OnCollisionEnter(PlayerController player, Collider2D col);
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
