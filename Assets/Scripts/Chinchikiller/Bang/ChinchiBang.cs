using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChinchiBang : MonoBehaviour
{
    private Animator Animator;
    private Rigidbody2D Rigidbody2D;
    public Transform FirePoint;
    public Transform ScopeSpawn;
    public GameObject Scope;
    public GameObject Misile;
    public string SearchForTag;
    public string AnimChinsil;
    public NewMovement GMove;
    private float prevMax;
    [HideInInspector] public Animator CSAnim;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        CSAnim = GetComponent<Animator>();
        prevMax = GMove.MaxSpeed;
    }

    // Update is called once per frame
    void OnBang()
    {
        //Misil
        BangLvl bang = transform.parent.GetComponent<BangLvl>();

        if (!CSAnim.GetCurrentAnimatorStateInfo(0).IsName(AnimChinsil) && !GMove.Animator.GetCurrentAnimatorStateInfo(0).IsName(GMove.AnimWalk) && !GameObject.FindWithTag(SearchForTag) && bang.tryBang())
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

    private void Update()
    {
        if (!GameObject.FindWithTag(SearchForTag))
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
        GameObject misile = Instantiate(Misile, FirePoint.position, Quaternion.identity, transform.parent);

    }
}
