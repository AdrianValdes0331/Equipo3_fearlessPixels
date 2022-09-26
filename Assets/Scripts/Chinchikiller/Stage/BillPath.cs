using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillPath : MonoBehaviour
{
    public Vector3[] currentPoint = new Vector3[4];
    int targetIndex = 0;
    public float speed;
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentPoint[targetIndex], Time.deltaTime * speed);
        if (transform.position == currentPoint[targetIndex])
            targetIndex = (targetIndex + 1) % currentPoint.Length;
    }
}
