using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashScript : MonoBehaviour
{

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
        animator.SetBool("TrashCollision", true);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        animator.SetBool("TrashCollision", false);
    }
}
