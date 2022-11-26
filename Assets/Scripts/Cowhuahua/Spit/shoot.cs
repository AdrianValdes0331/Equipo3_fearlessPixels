using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine;
using System;

public class shoot : MonoBehaviour
{
    public GameObject ToxSpit;
    public Transform point;

    void OnSpecial()
    {
        Instantiate(ToxSpit, point.position, point.rotation, transform.parent);
    }
}
