using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour, IHitboxResponder
{

    [SerializeField] private float dmg;
    [SerializeField] private Hitbox hitbox;
    [SerializeField] private int force;
    [SerializeField] private int angle;

    // Start is called before the first frame update
    void Start()
    {
        //hitbox = Instantiate(hitbox, gameObject.transform, false);
        attack();
    }

    // Update is called once per frame
    void Update()
    {
     
        hitbox.hitboxUpdate();

    }

    public void attack() {

        hitbox.openCollissionCheck();
        hitbox.setResponder(this);
    
    }

    public void CollisionedWith(Collider2D collider)
    {

        Hurtbox hurtbox = collider.GetComponent<Hurtbox>();
        //hurtbox?.getHitBy(dmg);

    }

}
