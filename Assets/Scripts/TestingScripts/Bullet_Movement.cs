using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Movement : MonoBehaviour
{
    public float Speed;
    private Rigidbody2D Rigidbody2D;
    private Vector2 Direction;
    [SerializeField] private float radio;
    [SerializeField] private float force;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Rigidbody2D.velocity = Direction * Speed;
        //Destroy(this.gameObject, 0.05f);
    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            print("HITTTT");
            Explosion();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radio);
    }
    public void Explosion()
    {       
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, radio);
        foreach (Collider2D collisionador in objects)
        {
            Rigidbody2D rb2D = collisionador.GetComponent<Rigidbody2D>();
            if (rb2D != null)
            {
                print("negatoive");
                Vector2 direction = collisionador.transform.position - transform.position;
                float distance = 1 + direction.magnitude;
                float finalForce = force / distance;
                rb2D.AddForce(direction * finalForce);
            }

        }
        DestroyBullet();
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
