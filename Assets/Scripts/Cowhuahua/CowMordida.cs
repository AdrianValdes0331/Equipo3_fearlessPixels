using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowMordida : MonoBehaviour, IHitboxResponder
{
    public NewMovement GMove;
    [HideInInspector] public Animator MordAnim;
    public string AnimMordida;
    [SerializeField] private float dmg;
    [SerializeField] private Hitbox hitbox;
    [SerializeField] private int force;
    [SerializeField] private int angle;
    private bool uHitbox;
    // Start is called before the first frame update
    void Start()
    {
        MordAnim = GetComponent<Animator>();
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
    void OnStrongKick()
    {
        //kick
        if (!MordAnim.GetCurrentAnimatorStateInfo(0).IsName(AnimMordida))
        {
            hitbox.openCollissionCheck();
            uHitbox = true;
            GMove.Animator.SetBool(GMove.AnimWalk, false);
            GMove.Animator.SetTrigger(AnimMordida);
        }
    }

    void DisableKick()
    {

        uHitbox = false;
        hitbox.closeCollissionCheck();

    }

    public void CollisionedWith(Collider2D collider)
    {
        if (collider.name == "CowhuaHurtbox") { return; }
        Hurtbox hurtbox = collider.GetComponent<Hurtbox>();
        if (hurtbox != null)
        {
            Debug.Log("Hit player");
            hurtbox.getHitBy(dmg, force, angle, transform.position.x);
        }
    }
}
