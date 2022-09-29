using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour, IHitboxResponder
{

    [SerializeField] private float dmg;
    [SerializeField] private Hitbox hitbox;
    [SerializeField] private int force;
    [SerializeField] private int angle;

    // Start is called before the first frame update
    void Start()
    {
        hitbox.openCollissionCheck();
        hitbox.setResponder(this);
        Destroy(gameObject, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        hitbox.hitboxUpdate();
    }

 /*   private void explode()
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

    public void CollisionedWith(Collider2D collider)
    {
        Vector2 direction = collider.transform.position - transform.position;
        float distance = 1 + direction.magnitude;
        float finalForce = force / distance;
        float finalDmg = dmg / distance;
        Hurtbox hurtbox = collider.GetComponent<Hurtbox>();
        Debug.Log("Explosion");
        if (hurtbox != null)
        {
            BangLvl bang = gameObject.transform.parent.GetComponent<BangLvl>();
            if(collider.transform.parent.transform.parent != transform.parent){
                bang.bangUpdate(finalDmg, true);
            }
            hurtbox.getHitBy(finalDmg, (int)finalForce, angle, transform.position.x);
        }
    }


}
