using System.Collections.Generic;
using UnityEngine.Playables;
using System.Collections;
using UnityEngine;
using System;

public class DummyKick : MonoBehaviour, IHitboxResponder
{     
    [SerializeField] private float dmg;
    [SerializeField] private Hitbox hitbox;
    [SerializeField] private int force;
    [SerializeField] private int angle;
    private bool uHitbox;
    // Start is called before the first frame update
    void Start()
    {
        uHitbox = false;       
        hitbox.setResponder(this);
    }

    void Update()
    {
        hitbox.openCollissionCheck();
        uHitbox = true;
        if (uHitbox)
        {
            hitbox.hitboxUpdate();
        }

    }

    public void CollisionedWith(Collider2D collider)
    {

        if (collider.transform.parent.transform.parent == transform.parent) { return; }
        Hurtbox hurtbox = collider.GetComponent<Hurtbox>();
        if (hurtbox != null)
        {
            BangLvl bang = transform.parent.GetComponent<BangLvl>();
            bang.bangUpdate(dmg, true);
            Debug.Log("Hit player");
            hurtbox.getHitBy(dmg, force, angle, transform.position.x);
        }
        else
        {
            NoPlayersHurtbox noPlayersHurtbox = collider.GetComponent<NoPlayersHurtbox>();
            if (noPlayersHurtbox != null)
            {
                noPlayersHurtbox.getHitBy(dmg, force, angle, transform.position.x);
            }
        }
    }
}
