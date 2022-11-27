using System.Collections;
using UnityEngine.Playables;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TickTickBoom : MonoBehaviour
{
    [HideInInspector] public Animator Animator;
    public Rigidbody2D rbody;
    public float LaunchForce;
    public GameObject Cabooommmmm;
    public GameObject timeup;
    // Start is called before the first frame update
    void Start()
    {
        rbody.freezeRotation = false;
        Vector3 scale = transform.parent.transform.GetChild(0).transform.localScale;
        rbody.gravityScale = 5;
        rbody.velocity = new Vector3(LaunchForce * scale.x * (1 / scale.y), 17, 0);
        StartCoroutine(HoldIt());
    }
    void bamm()
    {
        GameObject EXSound = Instantiate(timeup, transform.position, transform.rotation, transform.parent);
        GameObject cabom = Instantiate(Cabooommmmm, transform.position, transform.rotation, transform.parent);
        Destroy(cabom, 2.0f);
        Destroy(EXSound, 2.0f);
        Destroy(gameObject);
    }

    IEnumerator HoldIt()
    {
        yield return new WaitForSeconds(5f);
        bamm();
    }
}

