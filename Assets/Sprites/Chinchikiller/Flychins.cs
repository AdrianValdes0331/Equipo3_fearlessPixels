using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flychins : MonoBehaviour
{
    public float PlayerSpeed = 5;

    void Start()
    {
        Destroy(gameObject, 18f);
    }

    void Update()
    {
        transform.Translate(Vector3.right * PlayerSpeed, Space.World);
    }
}
