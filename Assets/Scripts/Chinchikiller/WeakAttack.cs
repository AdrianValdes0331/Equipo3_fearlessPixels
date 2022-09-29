using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        SWAnim = GetComponent<Animator>();
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
        Debug.Log("weak attack");
        if (!SWAnim.GetCurrentAnimatorStateInfo(0).IsName(AnimSword))
        {
            hitbox.openCollissionCheck();
            uHitbox = true;
            GMove.Animator.SetBool(GMove.AnimWalk, false);
            GMove.Animator.SetTrigger(AnimSword);
        }
    }

    void DisableKick()
    {

        uHitbox = false;
        hitbox.closeCollissionCheck();

    }

    public void CollisionedWith(Collider2D collider)
    {
        if (collider.name == "ChinchiHurtbox") { return; }
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
