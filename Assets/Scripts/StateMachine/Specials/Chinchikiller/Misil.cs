using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Misil : MonoBehaviour, IHitboxResponder
{
    [HideInInspector] public GameObject target;

    public event Action exit;

    public float speed = 5f;
    public float rotateSpeed = 200f;

    private Rigidbody2D rb;
    public GameObject ItsAHit;
    public GameObject ItsNotAHit;
    public GameObject Cabooommmmm;
    [HideInInspector] public Transform player;
    [SerializeField] private bool isBang;
    [SerializeField] private float TimeToChangeColor;
    [SerializeField] private float TimeToExplode;
    [SerializeField] private float dmg;
    [SerializeField] private NHitbox hitbox = new NHitbox();
    [SerializeField] private float radio;
    [SerializeField] private int force;
    [SerializeField] private Color colorToTurnTo = Color.red;
    [SerializeField] private int angle;
    [HideInInspector] public BangLvl bang;

    // Use this for initialization
    void Start()
    {
        //bang = transform.parent.GetComponent<BangLvl>();
        if (isBang)
        {
            dmg = bang.bangModifier(dmg);
        }
        transform.Rotate(Vector3.forward * -90);
        rb = GetComponent<Rigidbody2D>();
        hitbox.transform = transform;
        hitbox.Start();
        hitbox.setResponder(this);
        hitbox.openCollissionCheck();
        Invoke("ChangeColor", TimeToChangeColor);
        Invoke("DestroyBullet", TimeToExplode);
    }

    void FixedUpdate()
    {

        Vector2 direction = (Vector2)target.transform.position - rb.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, (transform.right * -1)).z;

        rb.angularVelocity = -rotateAmount * rotateSpeed;

        rb.velocity = (transform.right * -1) * speed;

        hitbox.hitboxUpdate();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radio);
    }

    private void ChangeColor()
    {
        GetComponent<Renderer>().material.color = colorToTurnTo;
    }

    private void DestroyBullet()
    {
        GameObject cabom = Instantiate(Cabooommmmm, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(cabom, 2.0f);
        Destroy(target);
        exit?.Invoke();
    }

    public void CollisionedWith(Collider2D collider)
    {
        if (collider.transform.parent.transform.parent == player) { return; }
        Destroy(gameObject);
        Destroy(GameObject.FindWithTag("scope"));
        print("HITTTT");

        //Explosion();
        NHurtbox hurtbox = collider.GetComponent<NHurtbox>();
        if (hurtbox != null)
        {
            GameObject EXSound = Instantiate(ItsAHit, transform.position, transform.rotation);
            GameObject cabom = Instantiate(Cabooommmmm, transform.position, transform.rotation);
            cabom.GetComponent<Explode>().enabled = false;
            Destroy(cabom, 2.0f);
            Destroy(EXSound, 2.0f);
            //BangLvl bang = gameObject.transform.parent.GetComponent<BangLvl>();
            bang.bangUpdate(dmg, true);
            hurtbox.getHitBy(dmg, force, angle, transform.position.x);
            exit?.Invoke();
        }
        else
        {
            GameObject EXSound2 = Instantiate(ItsNotAHit, transform.position, transform.rotation);
            Destroy(EXSound2, 2.0f);
            NoPlayersHurtbox noPlayersHurtbox = collider.GetComponent<NoPlayersHurtbox>();
            if (noPlayersHurtbox != null)
            {
                noPlayersHurtbox.getHitBy(dmg, force, angle, transform.position.x);
            }
            Instantiate(Cabooommmmm, transform.position, transform.rotation).GetComponent<Explode>().enabled = true;
            exit?.Invoke();
        }
    }
}
