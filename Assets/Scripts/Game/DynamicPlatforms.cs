using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicPlatforms : MonoBehaviour
{
    GameObject collisionObject;

    private void OnCollisionEnter2D(Collision2D collisionDetected)
    {
        collisionObject = collisionDetected.gameObject;
        if (collisionObject.CompareTag("Driver"))
        {
            collisionObject.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collisionDetected)
    {
        collisionObject = collisionDetected.gameObject;
        if (collisionObject.CompareTag("Driver"))
        {
            collisionObject.transform.parent = null;
            collisionObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
