using System.Collections.Generic;
using UnityEngine.Playables;
using System.Collections;
using UnityEngine;
using System;

public class Chinsil : MonoBehaviour
{
    private Animator Animator;
    private Rigidbody2D Rigidbody2D;
    public Transform FirePoint;
    public Transform ScopeSpawn;
    public GameObject Scope;
    public GameObject Misile;
    public string SearchForTag;
    public string AnimChinsil;  
    public Generic_Movements GMove;
    private float prevMax;
    [HideInInspector] public Animator CSAnim;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        CSAnim = GetComponent<Animator>();
        prevMax = GMove.MaxSpeed;
    }

    // Update is called once per frame
    void Update()
    {  
        //Misil
        if ((Input.GetButton("Fire1") || Input.GetKey(KeyCode.X)) && !CSAnim.GetCurrentAnimatorStateInfo(0).IsName(AnimChinsil) && !GMove.Animator.GetCurrentAnimatorStateInfo(0).IsName(GMove.AnimWalk) && !GameObject.FindWithTag(SearchForTag))
        {
            GMove.Animator.SetBool(GMove.AnimWalk, false);
            GMove.Animator.SetBool(AnimChinsil, true);
            GMove.MaxSpeed = 0.0f;
            Shoot();
        }
        else if (!GameObject.FindWithTag(SearchForTag))
        {
            CSAnim.SetBool(AnimChinsil, false);
            GMove.MaxSpeed = prevMax;
        }
    }

    //Shoot misile
    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 0.15f)
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.left;
        }

        GameObject scope = Instantiate(Scope, ScopeSpawn.position, Quaternion.identity);
        GameObject misile = Instantiate(Misile, FirePoint.position, Quaternion.identity);

    }
}
