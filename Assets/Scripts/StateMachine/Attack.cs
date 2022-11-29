using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public abstract class Attack : IPlayerBaseState, IHitboxResponder
{
    protected gizmo g;
    [SerializeField] protected float dmg;
    [SerializeField] protected int force;
    [SerializeField] private int angle;
    [SerializeField] protected float activeTime;
    [SerializeField] protected NHitbox hitbox = new NHitbox();
    protected Transform transform;
    protected float multiplier = 1f;


    public abstract void EnterState(PlayerController player);
    public abstract void Update(PlayerController player);
    public abstract void LateUpdate(PlayerController player);
    public abstract void OnTriggerEnter(PlayerController player, Collider2D col);
    public abstract void OnCollisionEnter(PlayerController player, Collision2D col);
    public abstract void Move(PlayerController player, Vector2 val, float speed);
    public abstract void Jump(PlayerController player, float speed);
    public abstract void OnCollisionEffects(); //Things to do after an attack hits dependent on implementation
    public abstract void OnNeutral(PlayerController player);
    public abstract void OnCharged(PlayerController player);
    public abstract void OnChargedCharged(PlayerController player);
    public abstract void OnRecovery(PlayerController player);
    public abstract void OnHit(PlayerController player);
    public abstract void OnEnable(PlayerController player);
    public abstract void OnDisable(PlayerController player);
    public abstract void OnSpecial(PlayerController player);
    public abstract void OnSpecialHold(PlayerController player);
    public abstract void OnBang(PlayerController player);
    public gizmo? gz(){return g;}
    //All attacks have the same implementation for CollisionedWith
    public void CollisionedWith(Collider2D collider)
    {
        OnCollisionEffects();
        //if the collider's grandparent is the parent of the attack's gameObject
        //return without doing anything
        if(collider.transform.parent.transform.parent == transform) { return; } 
        NHurtbox hurtbox = collider.GetComponent<NHurtbox>();
        //if the collider has a hurtbox
        if (hurtbox != null)
        {
            BangLvl bang = transform.gameObject.GetComponent<BangLvl>();
            bang.bangUpdate(dmg, true);
            Debug.Log("Hit player");
            hurtbox.getHitBy(dmg*multiplier, (int)(force * multiplier), angle, transform.position.x);
        }
    }
    public bool hasGizmos()
    {
        return true;
    }

}
