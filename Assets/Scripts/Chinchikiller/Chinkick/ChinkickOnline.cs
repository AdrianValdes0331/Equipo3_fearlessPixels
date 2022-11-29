using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChinkickOnline : MonoBehaviour, IHitboxResponder
{
    public NewMovement GMove;
    [HideInInspector] public Animator CKAnim;
    public string AnimChinkick;
    [SerializeField] private float dmg;
    [SerializeField] private Hitbox hitbox;
    [SerializeField] private int force;
    [SerializeField] private int angle;
    private bool uHitbox;
    // Start is called before the first frame update
    void Start()
    {
        uHitbox = false;
        CKAnim = GetComponent<Animator>();
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
    //[PunRPC]
    void OnStrongKick()
    {
        //kick
        if (!CKAnim.GetCurrentAnimatorStateInfo(0).IsName(AnimChinkick))
        {
            hitbox.openCollissionCheck();
            uHitbox = true;
            GMove.Animator.SetTrigger(AnimChinkick);
            GMove.Animator.SetBool(GMove.AnimWalk, false);
        }
    }

    void DisableKick()
    {

        uHitbox = false;
        hitbox.closeCollissionCheck();

    }

    public void CollisionedWith(Collider2D collider)
    {
        if (collider.transform.parent.transform.parent == transform.parent) { return; }
        MultiplayerHurtbox hurtbox = collider.GetComponent<MultiplayerHurtbox>();
        if (hurtbox != null)
        {
            //BangLvl bang = transform.parent.GetComponent<BangLvl>();
            //bang.bangUpdate(dmg, true);
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