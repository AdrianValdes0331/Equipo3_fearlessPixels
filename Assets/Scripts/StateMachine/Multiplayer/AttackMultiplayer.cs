using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public abstract class AttackMultiplayer : IMultiplayerBaseState, IHitboxResponder
{
    protected gizmo g;
    [SerializeField] protected float dmg;
    [SerializeField] protected int force;
    [SerializeField] private int angle;
    [SerializeField] protected float activeTime;
    [SerializeField] protected NHitbox hitbox = new NHitbox();
    protected Transform transform;
    protected float multiplier = 1f;


    public abstract void EnterState(MultiplayerControllerSM player);
    public abstract void Update(MultiplayerControllerSM player);
    public abstract void LateUpdate(MultiplayerControllerSM player);
    public abstract void OnTriggerEnter(MultiplayerControllerSM player, Collider2D col);
    public abstract void OnCollisionEnter(MultiplayerControllerSM player, Collision2D col);
    public abstract void Move(MultiplayerControllerSM player, Vector2 val, float speed);
    public abstract void Jump(MultiplayerControllerSM player, float speed);
    public abstract void OnCollisionEffects(); //Things to do after an attack hits dependent on implementation
    public abstract void OnNeutral(MultiplayerControllerSM player);
    public abstract void OnCharged(MultiplayerControllerSM player);
    public abstract void OnChargedCharged(MultiplayerControllerSM player);
    public abstract void OnRecovery(MultiplayerControllerSM player);
    public abstract void OnHit(MultiplayerControllerSM player);
    public abstract void OnEnable(MultiplayerControllerSM player);
    public abstract void OnDisable(MultiplayerControllerSM player);
    public abstract void OnSpecial(MultiplayerControllerSM player);
    public abstract void OnSpecialHold(MultiplayerControllerSM player);
    public abstract void OnBang(MultiplayerControllerSM player);
    public gizmo? gz(){return g;}
    //All attacks have the same implementation for CollisionedWith
    public void CollisionedWith(Collider2D collider)
    {
        OnCollisionEffects();
        //if the collider's grandparent is the parent of the attack's gameObject
        //return without doing anything
        if(collider.transform.parent.transform.parent == transform) { return; } 
        NHurtboxMultiplayer hurtbox = collider.GetComponent<NHurtboxMultiplayer>();
        //if the collider has a hurtbox
        if (hurtbox != null)
        {
            BangLvlMultiplayer bang = transform.gameObject.GetComponent<BangLvlMultiplayer>();
            bang.bangUpdate(dmg, true);
            Debug.Log("Hit player");
            hurtbox.photonView.RPC("getHitBy", Photon.Pun.RpcTarget.All, dmg * multiplier, ((int)(force * multiplier)), angle, transform.position.x);
            //hurtbox.getHitBy(dmg*multiplier, (int)(force * multiplier), angle, transform.position.x);
        }
    }
    public bool hasGizmos()
    {
        return true;
    }

}
