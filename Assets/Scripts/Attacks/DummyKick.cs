using System.Collections.Generic;
using UnityEngine.Playables;
using System.Collections;
using UnityEngine;
using System;

public class DummyKick : MonoBehaviour, IHitboxResponder
{     
    [SerializeField] private float dmg;
    [SerializeField] private NHitbox hitbox;
    [SerializeField] private int force;
    [SerializeField] private int angle;
    private bool uHitbox;
    // Start is called before the first frame update
    void Start()
    {
        uHitbox = true;       
        hitbox.setResponder(this);

        //pSize = System.Math.Abs(player.transform.localScale.x);
        //queued = false;
        //done = false;
        //transform = player.transform;
        hitbox.transform = transform;
        MonoBehaviour.print("Entering Neutral");
        //player.SetAnimatorTrigger(PlayerController.AnimStates.Neutral);
        hitbox.Start();
        hitbox.setResponder(this);
        hitbox.openCollissionCheck();

        //g.sz = hitbox.sz;
        //g.isSphere = hitbox.isSphere;
        //g.radius = hitbox.radius;
        //g.pos = hitbox.pos;
        //player.StartCoroutine(Active(player, activeTime));
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
        NHurtbox hurtbox = collider.GetComponent<NHurtbox>();
        if (hurtbox != null)
        {
            //BangLvl bang = transform.parent.GetComponent<BangLvl>();
            //bang.bangUpdate(dmg, true);
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
