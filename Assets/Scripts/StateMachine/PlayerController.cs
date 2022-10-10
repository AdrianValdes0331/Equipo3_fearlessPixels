using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [HideInInspector] public Animator animator;
    [HideInInspector] public Rigidbody2D rb;
    private PlayerBaseState currState;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       currState.Update(this); 
    }
}
