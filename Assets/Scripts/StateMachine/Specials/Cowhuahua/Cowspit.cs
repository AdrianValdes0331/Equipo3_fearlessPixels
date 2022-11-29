using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cowspit : MonoBehaviour, IHitboxResponder
{
    [HideInInspector] public Transform player;
    [SerializeField] private float dmg;
    [SerializeField] private NHitbox hitbox;
    [SerializeField] private int force;
    [SerializeField] private int angle;
    private Rigidbody2D rbody;
    private bool uHitbox;
    public float LaunchForce;
    [HideInInspector] public Vector3 scale;

    // Start is called before the first frame update
    void Start()
    {
        uHitbox = false;
        //Animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
        rbody.freezeRotation = true;
        Debug.Log(transform.parent.transform.rotation.x);
        rbody.gravityScale = 5;
        rbody.velocity = new Vector3(LaunchForce * scale.x * (1 / scale.y), 10, 0);
        hitbox.transform = transform;
        hitbox.Start();
        hitbox.setResponder(this);
        hitbox.openCollissionCheck();

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
            //Animator.SetTrigger(SpitName);
            Destroy(gameObject, 1.5f);
        }
    }

    public void CollisionedWith(Collider2D collider)
    {

        if (collider.transform.parent.transform.parent == transform.parent) { return; }
        NHurtbox hurtbox = collider.GetComponent<NHurtbox>();
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
