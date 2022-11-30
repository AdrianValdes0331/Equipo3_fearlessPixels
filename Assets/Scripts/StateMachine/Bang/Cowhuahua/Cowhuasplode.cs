using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cowhuasplode : MonoBehaviour, IHitboxResponder
{
    public static event Action bangEnd;
    public event Action exit;

    public GameObject Cabooommmmm;
    private BangLvl bang;
    Animator anim;
    [HideInInspector] public Transform player;
    [SerializeField] private float dmg;
    [SerializeField] private NHitbox hitbox;
    [SerializeField] private int force;
    [SerializeField] private int angle;
    [SerializeField] private bool isBang;
    private Rigidbody2D rbody;
    private bool uHitbox;
    public float LaunchForce;
    public string SpitName = "none";
    [HideInInspector] public Vector3 scale;
    // Start is called before the first frame update
    void Start()
    {
        bang = player.gameObject.GetComponent<BangLvl>();
        if (isBang)
        {
            dmg = bang.bangModifier(dmg);
        }
        uHitbox = false;
        hitbox.setResponder(this);
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
        rbody.freezeRotation = true;
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
            anim.SetTrigger(SpitName);
            InvokeRepeating("Tick", 0, 0.5f);
            StartCoroutine(waitAfterBang());
            Destroy(gameObject, 5f);
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
        GameObject cabom = Instantiate(Cabooommmmm, transform.position, transform.rotation);
        NHurtbox hurtbox = collider.GetComponent<NHurtbox>();
        if (hurtbox != null)
        {
            bang.bangUpdate(dmg, true);
            Debug.Log("Hit player");
            cabom.GetComponent<ExplodSM>().enabled = false;
            Destroy(cabom, 2.0f);
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
            Destroy(cabom, 2.0f);
            exit?.Invoke();
        }
    }
    void Tick()
    {
        hitbox.openCollissionCheck();
    }

    IEnumerator waitAfterBang()
    {
        yield return new WaitForSeconds(7);
        bangEnd?.Invoke();
    }

}
