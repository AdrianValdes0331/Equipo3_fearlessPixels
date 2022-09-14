using System.Collections.Generic;
using UnityEngine.Playables;
using System.Collections;
using UnityEngine;
using System;

public class MisilFalling : MonoBehaviour, IHitboxResponder
{
    //public float Speed;
    private Rigidbody2D Rigidbody2D;
    private Vector2 Direction;
    private Renderer rend;
    public float TimeToChangeColor;
    public float TimeToExplode;   

    public GameObject Cabooommmmm;
    [SerializeField] private float dmg;
    [SerializeField] private Hitbox hitbox;
    [SerializeField] private float radio;
    [SerializeField] private float force;
    [SerializeField] private Color colorToTurnTo = Color.red;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        //Animator = GetComponent<Animator>();
        Invoke("ChangeColor", TimeToChangeColor);
        Invoke("DestroyBullet", TimeToExplode);
    }   

    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }
/*    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag != "Player" && col.gameObject.tag != "scope")
        {
            print("HITTTT");
            Explosion();
        }
    }*/
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
    }

    public void ChangeColor()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = colorToTurnTo;
    }

    public void DestroyBullet()
    {        
        GameObject cabom = Instantiate(Cabooommmmm, transform.position, transform.rotation);     
        Destroy(gameObject);
        Destroy(cabom, 2.0f);

        Destroy(GameObject.FindWithTag("scope"));
    }

    public void CollisionedWith(Collider2D collider)
    {

        print("HITTTT");
        Explosion();
        Hurtbox hurtbox = collider.GetComponent<Hurtbox>();
        //hurtbox?.getHitBy(dmg);

    }

}
