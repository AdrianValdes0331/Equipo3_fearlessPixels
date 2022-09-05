using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour, IHitboxResponder
{

    public int dmg;
    public Hitbox hitbox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void attack() {

        hitbox.setResponder(this);
    
    }

    public void CollisionedWith(Collider2D collider)
    {

        Hurtbox hurtbox = collider.GetComponent<Hurtbox>();
        hurtbox?.getHitBy(dmg);

    }

}
