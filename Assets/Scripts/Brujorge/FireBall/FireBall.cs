using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    Rigidbody2D fireBallRigidbody;
    Vector3 lastVelocity;
    int collisionCounter = 0;
    float upperLimit, bottomLimit, leftLimit, rightLimit, angle;
    float speed = 7.5f;
    Vector3 fireBallPos, direction;
    Transform focusTarget;

    // Start is called before the first frame update
    void Start()
    {
        focusTarget = transform.parent.Find("FocusTarget");
        upperLimit = GameObject.Find("EdgeLimits/UpperLimit").transform.position.y;
        bottomLimit = GameObject.Find("EdgeLimits/BottomLimit").transform.position.y;
        leftLimit = GameObject.Find("EdgeLimits/LeftLimit").transform.position.x;
        rightLimit = GameObject.Find("EdgeLimits/RightLimit").transform.position.x;
        fireBallRigidbody = GetComponent<Rigidbody2D>();
        
        fireBallRigidbody.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {

        fireBallPos = transform.position;
        if (fireBallPos.y < bottomLimit || fireBallPos.y > upperLimit || fireBallPos.x < leftLimit || fireBallPos.x > rightLimit || collisionCounter >= 5 || (collisionCounter > 0 && lastVelocity.magnitude < 7.5))
        {
            Destroy(transform.parent.gameObject);
        }
        
        if (collisionCounter > 0 && transform.rotation != Quaternion.AngleAxis(angle, Vector3.forward))
        {
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        lastVelocity = fireBallRigidbody.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(transform.parent.gameObject);
        }
        speed = lastVelocity.magnitude * 1.25f;
        direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
        fireBallRigidbody.velocity = direction * Mathf.Max(speed, 0f);
        focusTarget.position = direction;
        angle = Mathf.Atan2(focusTarget.position.y, focusTarget.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        collisionCounter ++;
    }
}
