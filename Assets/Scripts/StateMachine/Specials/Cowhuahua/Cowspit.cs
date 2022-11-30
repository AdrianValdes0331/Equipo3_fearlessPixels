using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cowspit : MonoBehaviour, IHitboxResponder
{
    public event Action exit;

    Animator anim;
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
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
        rbody.freezeRotation = true;
        //Debug.Log(transform.parent.transform.rotation.x);
        rbody.gravityScale = 5;
        rbody.velocity = new Vector3(LaunchForce * scale.x * (1 / scale.y), 10, 0);
        hitbox.transform = transform;
        hitbox.Start();
        hitbox.setResponder(this);
        hitbox.openCollissionCheck();

    }
    void Update()
    {
        uHitbox = true;
        if (uHitbox)
        {
            hitbox.hitboxUpdate();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.transform.parent != player)
        {
            exit?.Invoke();
            //Debug.Log("Boom");
            //Debug.Log(collision.collider.transform.parent);
            //rbody.freezeRotation = true;
            rbody.velocity = Vector3.zero;
            anim.SetTrigger("Sploosh");
            InvokeRepeating("Tick", 0, 0.1f);
            Destroy(gameObject, 1.5f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = hitbox.currColor;
        Gizmos.matrix = Matrix4x4.TRS(hitbox.pos, transform.rotation, transform.localScale);
        if (!hitbox.isSphere)
        {
            Gizmos.DrawCube(Vector3.zero, new Vector3(hitbox.sz.x * 2, hitbox.sz.y * 2, 0)); // Because size is halfExtents
        }
        else
        {
            Gizmos.DrawSphere(Vector3.zero, hitbox.radius);
        }

    }

    public void CollisionedWith(Collider2D collider)
    {

        if (collider.transform.parent.transform.parent == player) { return; }
        NHurtbox hurtbox = collider.GetComponent<NHurtbox>();
        if (hurtbox != null)
        {
            BangLvl bang = player.gameObject.GetComponent<BangLvl>();
            bang.bangUpdate(dmg, true);
            Debug.Log("Hit player");
            hurtbox.getHitBy(dmg, force, angle, transform.position.x);
            exit?.Invoke();
        }
        else
        {
            NoPlayersHurtbox noPlayersHurtbox = collider.GetComponent<NoPlayersHurtbox>();
            if (noPlayersHurtbox != null)
            {
                noPlayersHurtbox.getHitBy(dmg, force, angle, transform.position.x);
            }
            exit?.Invoke();
        }
    }

    void Tick()
    {
        hitbox.openCollissionCheck();
    }

}
