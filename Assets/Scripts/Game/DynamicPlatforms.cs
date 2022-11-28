using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicPlatforms : MonoBehaviour
{
    GameObject collisionObject;
/*    public static event Action<Rigidbody2D> playerOn;
    public static event Action<Rigidbody2D> playerOff;*/

    private void OnCollisionEnter2D(Collision2D collisionDetected)
    {
        collisionObject = collisionDetected.gameObject;
        if (collisionObject.CompareTag("Player"))
        {
            collisionObject.transform.parent.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collisionDetected)
    {
        GameObject collisionObject = collisionDetected.gameObject;
        if (collisionObject.CompareTag("Player"))
        {
            collisionObject.transform.parent.parent = null;
        }
    }
}
