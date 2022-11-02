using System.Collections.Generic;
using UnityEngine.Playables;
using System.Collections;
using UnityEngine;
using System;

public class CowGolpeCargado : MonoBehaviour, IHitboxResponder
{
    public NewMovement GMove;
    [HideInInspector] public Animator CWAnim;
    public string AnimCowChargePunch;
    [SerializeField] private float dmg;
    [SerializeField] private Hitbox hitbox;
    [SerializeField] private int force;
    [SerializeField] private int angle;
    private bool uHitbox;
    // Start is called before the first frame update
    void Start()
    {
        uHitbox = false;
        CWAnim = GetComponent<Animator>();
        hitbox.setResponder(this);
    }

    void Update()
    {

        if (uHitbox)
        {
            hitbox.hitboxUpdate();
        }

    }

    // Update is called once per frame
    void OnWeakAttack()
    {
        //kick
        if (!CWAnim.GetCurrentAnimatorStateInfo(0).IsName(AnimCowChargePunch))
        {
            hitbox.openCollissionCheck();
            uHitbox = true;
            GMove.Animator.SetTrigger(AnimCowChargePunch);
            GMove.Animator.SetBool(GMove.AnimWalk, false);
        }
    }

    void DisableChargePunch()
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
    }
}
