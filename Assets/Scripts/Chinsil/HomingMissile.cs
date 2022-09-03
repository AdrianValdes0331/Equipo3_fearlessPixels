using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HomingMissile : MonoBehaviour
{

	public Transform target;

	public float speed = 5f;
	public float rotateSpeed = 200f;

	private Rigidbody2D rb;

	// Use this for initialization
	void Start()
	{
		target = GameObject.FindGameObjectWithTag("scope").transform;
		transform.Rotate(Vector3.forward * -90);
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
    {

        Vector2 direction = (Vector2)target.position - rb.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, (transform.right * -1)).z;

        rb.angularVelocity = -rotateAmount * rotateSpeed;

        rb.velocity = (transform.right * -1) * speed;
    }

    /*void OnTriggerEnter2D()
	{
		// Put a particle effect here
		Destroy(gameObject);
	}*/
}
