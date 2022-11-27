using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine;
using System;

public class shoot : MonoBehaviour
{
    [HideInInspector] public Animator Animator;
    public GameObject ToxSpit;
    public Transform point;

    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    void OnSpecial()
    {
        Animator.SetTrigger("CowSpit");
        Instantiate(ToxSpit, point.position, point.rotation, transform.parent);
    }
}
