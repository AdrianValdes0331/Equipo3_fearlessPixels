using System.Collections.Generic;
using UnityEngine.Playables;
using System.Collections;
using UnityEngine;
using System;

public class Chinkick : MonoBehaviour
{
    public NewMovement GMove;
    [HideInInspector] public Animator CKAnim;
    public string AnimChinkick;
    // Start is called before the first frame update
    void Start()
    {
        CKAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //kick
        if ((Input.GetButton("Fire3") || Input.GetKey(KeyCode.Z)) && !CKAnim.GetCurrentAnimatorStateInfo(0).IsName(AnimChinkick))
        {
            GMove.Animator.SetBool(GMove.AnimWalk, false);
            GMove.Animator.SetTrigger(AnimChinkick);
        }
    }
}
