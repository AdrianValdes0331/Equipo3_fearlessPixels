using System.Collections;
using UnityEngine.Playables;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BangExpode : MonoBehaviour, IHitboxResponder
{
    public GameObject Cabooommmmm;
    private BangLvl bang;
    [HideInInspector] public Animator Animator;
    [SerializeField] private float dmg;
    [SerializeField] private Hitbox hitbox;
    [SerializeField] private int force;
    [SerializeField] private int angle;
    [SerializeField] private bool isBang;
    public Rigidbody2D rbody;
    private bool uHitbox;
    public float LaunchForce;
    public string SpitName = "none";
    // Start is called before the first frame update
    void Start()
    {
        bang = transform.parent.GetComponent<BangLvl>();
        if (isBang)
        {
            dmg = bang.bangModifier(dmg);
        }
        uHitbox = false;
        hitbox.setResponder(this);
        Animator = GetComponent<Animator>();
        rbody.freezeRotation = true;
        Debug.Log(transform.parent.transform.rotation.x);
        Vector3 scale = transform.parent.transform.GetChild(0).transform.localScale;
        rbody.gravityScale = 5;
        rbody.velocity = new Vector3(LaunchForce * scale.x * (1 / scale.y), 10, 0);

    }
    void Update()
    {
        hitbox.openCollissionCheck();
        uHitbox = true;
        if (uHitbox)
        {
            hitbox.hitboxUpdate();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.transform.parent != transform.parent)
        {
            //Debug.Log("Boom");
            //Debug.Log(collision.collider.transform.parent);
            //rbody.freezeRotation = true;
            rbody.velocity = Vector3.zero;
            Animator.SetTrigger(SpitName);
            Destroy(gameObject, 5f);
        }
    }

    public void CollisionedWith(Collider2D collider)
    {
        if (collider.transform.parent.transform.parent == transform.parent) { return; }
        GameObject cabom = Instantiate(Cabooommmmm, transform.position, transform.rotation, transform.parent);
        Hurtbox hurtbox = collider.GetComponent<Hurtbox>();
        if (hurtbox != null)
        {
            BangLvl bang = transform.parent.GetComponent<BangLvl>();
            bang.bangUpdate(dmg, true);
            Debug.Log("Hit player");
            cabom.GetComponent<Explode>().enabled = false;
            Destroy(cabom, 2.0f);
            hurtbox.getHitBy(dmg, force, angle, transform.position.x);
        }
        else
        {
            NoPlayersHurtbox noPlayersHurtbox = collider.GetComponent<NoPlayersHurtbox>();
            if (noPlayersHurtbox != null)
            {
                noPlayersHurtbox.getHitBy(dmg, force, angle, transform.position.x);
            }
            Destroy(cabom, 2.0f);
        }
    }
}
