using System.Collections.Generic;
using UnityEngine.Playables;
using System.Collections;
using UnityEngine;
using System;

public class WeakAttack : MonoBehaviour, IHitboxResponder
{
    public NewMovement GMove;
    [HideInInspector] public Animator SWAnim;
    public string AnimSword;
    [SerializeField] private float dmg;
    [SerializeField] private Hitbox hitbox;
    [SerializeField] private int force;
    [SerializeField] private int angle;
    private bool uHitbox;
    // Start is called before the first frame update
    void Start()
    {
        uHitbox = false;
        SWAnim = GetComponent<Animator>();
        hitbox.setResponder(this);
    }

    void Update()
    {

        if (uHitbox)
        {
            hitbox.hitboxUpdate();
        }
        /*if (!SWAnim.GetCurrentAnimatorStateInfo(0).IsName("GolpeBasicoCK"))
        {
            uHitbox = false;
        }*/
    }

    // Update is called once per frame
    void OnWeakAttack()
    {
        //kick
        if (!SWAnim.GetCurrentAnimatorStateInfo(0).IsName(AnimSword))
        {
            hitbox.openCollissionCheck();
            uHitbox = true;
            GMove.Animator.SetTrigger(AnimSword);
            GMove.Animator.SetBool(GMove.AnimWalk, false);
        }
    }

    void DisableSword()
    {

        uHitbox = false;
        hitbox.closeCollissionCheck();

    }

    public void CollisionedWith(Collider2D collider)
    {

        if (collider.transform.parent.transform.parent == transform.parent) { return; }
        Hurtbox hurtbox = collider.GetComponent<Hurtbox>();
        if (hurtbox != null)
        {
            BangLvl bang = transform.parent.GetComponent<BangLvl>();
            bang.bangUpdate(dmg, true);
            Debug.Log("Hit player");
            hurtbox.getHitBy(dmg, force, angle, transform.position.x);
        }
        else
        {
            NoPlayersHurtbox noPlayersHurtbox = collider.GetComponent<NoPlayersHurtbox>();
            if (noPlayersHurtbox != null)
            {
                noPlayersHurtbox.getHitBy(dmg, force, angle, transform.position.x);
            }
        }
    }
}
