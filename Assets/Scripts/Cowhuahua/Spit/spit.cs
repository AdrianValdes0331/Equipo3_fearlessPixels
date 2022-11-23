using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spit : MonoBehaviour
{
    [HideInInspector] public Animator Animator;
    public Rigidbody2D rbody;
    public float LaunchForce;
    public string SpitName = "none";
    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        rbody.velocity = LaunchForce * transform.right;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Boom");
        Animator.SetTrigger(SpitName);
        Destroy(gameObject, 0.5f);
    }
}
