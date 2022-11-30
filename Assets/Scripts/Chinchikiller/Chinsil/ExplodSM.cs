using System.Collections;
//using Photon.Pun;
//using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class ExplodSM : MonoBehaviour, IHitboxResponder
{

    [HideInInspector] public Transform player;
    [SerializeField] private float dmg;
    [SerializeField] private NHitbox hitbox;
    [SerializeField] private int force;
    [SerializeField] private int angle;

    // Start is called before the first frame update
    void Start()
    {
        hitbox.transform = transform;
        hitbox.Start();
        hitbox.setResponder(this);
        hitbox.openCollissionCheck();
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
    //[PunRPC]
    public void CollisionedWith(Collider2D collider)
    {
        Vector2 direction = collider.transform.position - transform.position;
        float distance = 1 + direction.magnitude;
        float finalForce = force / distance;
        float finalDmg = dmg / distance;
        NHurtbox hurtbox = collider.GetComponent<NHurtbox>();
        if (hurtbox != null)
        {
            BangLvl bang = player.gameObject.GetComponent<BangLvl>();
            if(collider.transform.parent.transform.parent != player){
                bang.bangUpdate(finalDmg, true);
            }
            hurtbox.getHitBy(finalDmg, (int)finalForce, angle, transform.position.x);
        }
        else
        {
            NoPlayersHurtbox noPlayersHurtbox = collider.GetComponent<NoPlayersHurtbox>();
            if (noPlayersHurtbox != null)
            {
                noPlayersHurtbox.getHitBy(finalDmg, (int)finalForce, angle, transform.position.x);
            }
        }
    }


}
