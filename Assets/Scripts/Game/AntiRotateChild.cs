using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiRotateChild : MonoBehaviour
{
    public Transform child;

    void Update()
    {
        child.transform.localScale = new Vector3(gameObject.transform.localScale.x * -1.0f, 1.0f, 1.0f);
    }
}
