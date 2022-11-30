using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FireballSM : MonoBehaviour, IHitboxResponder
{

    Rigidbody2D fireBallRigidbody;
    Vector3 lastVelocity;
    int collisionCounter = 0;
    float upperLimit, bottomLimit, leftLimit, rightLimit, angle;
    float speed = 7.5f;
    Vector3 fireBallPos, direction;
    Transform focusTarget;
    [HideInInspector] public Transform player;
    [SerializeField] private float dmg;
    [SerializeField] private NHitbox hitbox;
    [SerializeField] private int force;
    //[SerializeField] private int pushAngle;

    // Start is called before the first frame update
    void Start()
    {
        focusTarget = transform.Find("FocusTarget");
        upperLimit = GameObject.Find("EdgeLimits/UpperLimit").transform.position.y;
        bottomLimit = GameObject.Find("EdgeLimits/BottomLimit").transform.position.y;
        leftLimit = GameObject.Find("EdgeLimits/LeftLimit").transform.position.x;
        rightLimit = GameObject.Find("EdgeLimits/RightLimit").transform.position.x;
        fireBallRigidbody = GetComponent<Rigidbody2D>();

        fireBallRigidbody.velocity = transform.right * speed;
        hitbox.transform = transform;
        hitbox.Start();
        hitbox.setResponder(this);
        hitbox.openCollissionCheck();
    }

    // Update is called once per frame
    void Update()
    {

        fireBallPos = transform.position;
        if (fireBallPos.y < bottomLimit || fireBallPos.y > upperLimit || fireBallPos.x < leftLimit || fireBallPos.x > rightLimit || collisionCounter >= 10 || (collisionCounter > 0 && lastVelocity.magnitude < 7.5))
        {
            Destroy(gameObject);
        }

        if (collisionCounter > 0 && transform.rotation != Quaternion.AngleAxis(angle, Vector3.forward))
        {
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        lastVelocity = fireBallRigidbody.velocity;
        hitbox.hitboxUpdate();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = hitbox.currColor;
        Gizmos.matrix = Matrix4x4.TRS(hitbox.pos, transform.rotation, transform.localScale);
        if (!hitbox.isSphere)
        {
            Gizmos.DrawCube(Vector3.zero, new Vector3(hitbox.sz.x * 2, hitbox.sz.y * 2, 0)); // Because size is halfExtents
        }
        else
        {
            Gizmos.DrawSphere(Vector3.zero, hitbox.radius);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        speed = lastVelocity.magnitude * 1.25f;
        direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
        fireBallRigidbody.velocity = direction * Mathf.Max(speed, 0f);
        focusTarget.position = direction;
        angle = Mathf.Atan2(focusTarget.position.y, focusTarget.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        collisionCounter++;
    }

    public void CollisionedWith(Collider2D collider)
    {
        speed = lastVelocity.magnitude * 1.25f;
        direction = Vector3.Reflect(lastVelocity.normalized, new Vector3(collider.transform.parent.transform.parent.localScale.x, 0, 0));
        fireBallRigidbody.velocity = direction * Mathf.Max(speed, 0f);
        focusTarget.position = direction;
        angle = Mathf.Atan2(focusTarget.position.y, focusTarget.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        collisionCounter++;
        if (collider.transform.parent.transform.parent == player) 
        {
            return; 
        }
        NHurtbox hurtbox = collider.GetComponent<NHurtbox>();
        if (hurtbox != null)
        {
            BangLvl bang = player.gameObject.GetComponent<BangLvl>();
            bang.bangUpdate(dmg, true);
            Debug.Log("Hit player");
            hurtbox.getHitBy(dmg, force, (int)(angle), transform.position.x);
        }
        else
        {
            NoPlayersHurtbox noPlayersHurtbox = collider.GetComponent<NoPlayersHurtbox>();
            if (noPlayersHurtbox != null)
            {
                noPlayersHurtbox.getHitBy(dmg, force, (int)(angle), transform.position.x);
            }
        }
    }

}
