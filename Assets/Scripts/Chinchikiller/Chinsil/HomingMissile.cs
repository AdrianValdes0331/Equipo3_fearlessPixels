using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingMissile : MonoBehaviour, IHitboxResponder
{

	private Transform target;

	public float speed = 5f;
	public float rotateSpeed = 200f;

	private Rigidbody2D rb;

	public GameObject Cabooommmmm;
    [SerializeField] private bool isBang;
    [SerializeField] private float TimeToChangeColor;
    [SerializeField] private float TimeToExplode;
    [SerializeField] private float dmg;
	[SerializeField] private Hitbox hitbox;
	[SerializeField] private float radio;
	[SerializeField] private int force;
	[SerializeField] private Color colorToTurnTo = Color.red;
    [SerializeField] private int angle;
    private BangLvl bang; 

	// Use this for initialization
	void Start()
	{
        bang = transform.parent.GetComponent<BangLvl>();
        if (isBang)
        {
            dmg = bang.bangModifier(dmg);
        }
		target = GameObject.FindGameObjectWithTag("scope").transform;
		transform.Rotate(Vector3.forward * -90);
		rb = GetComponent<Rigidbody2D>();
        hitbox.openCollissionCheck();
        hitbox.setResponder(this);
        Invoke("ChangeColor", TimeToChangeColor);
        Invoke("DestroyBullet", TimeToExplode);
    }

	void FixedUpdate()
    {

        Vector2 direction = (Vector2)target.position - rb.position;

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
    /*private void Explosion()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, radio);
        foreach (Collider2D collisionador in objects)
        {
            Rigidbody2D rb2D = collisionador.GetComponent<Rigidbody2D>();
            if (rb2D != null)
            {
                //Vector2 direction = collisionador.transform.position - transform.position;
                //collisionador.GetComponent<Rigidbody2D>().AddForce(direction * force);
                Vector2 direction = collisionador.transform.position - transform.position;
                float distance = 1 + direction.magnitude;
                float finalForce = force / distance;
                Debug.Log(rb2D.name);
                Debug.Log(finalForce);
                rb2D.AddForce(direction * finalForce);
            }

        }
        DestroyBullet();
    }*/

    private void ChangeColor()
    {
        GetComponent<Renderer>().material.color = colorToTurnTo;
    }

    private void DestroyBullet()
    {
        GameObject cabom = Instantiate(Cabooommmmm, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(cabom, 2.0f);
        Destroy(GameObject.FindWithTag("scope"));
    }

    public void CollisionedWith(Collider2D collider)
    {
        if(collider.transform.parent.transform.parent == transform.parent) { return; }
        Destroy(gameObject);
        Destroy(GameObject.FindWithTag("scope"));
        print("HITTTT");
        //Explosion();
        Hurtbox hurtbox = collider.GetComponent<Hurtbox>();
        if (hurtbox != null)
        {
            Debug.Log("Hit player");
            GameObject cabom = Instantiate(Cabooommmmm, transform.position, transform.rotation, transform.parent);
            cabom.GetComponent<Explode>().enabled = false;
            Destroy(cabom, 2.0f);
            BangLvl bang = gameObject.transform.parent.GetComponent<BangLvl>();
            bang.bangUpdate(dmg, true);
            hurtbox.getHitBy(dmg, force, angle, transform.position.x);
        }
        else
        {
            Debug.Log("Hit obstacle");
            Instantiate(Cabooommmmm, transform.position, transform.rotation, transform.parent).GetComponent<Explode>().enabled = true;
        }
    }
}
