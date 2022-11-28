using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallArrowMovement : MonoBehaviour
{
    float rotationSpeed = 150f;

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles = new Vector3(0, 0, Mathf.PingPong(Time.time * rotationSpeed, 90) - 30);
    }
}
