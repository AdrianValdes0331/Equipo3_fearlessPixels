using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicPlatforms : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collisionDetected)
    {
        GameObject collisionObject = collisionDetected.gameObject;
        if (collisionObject.CompareTag("Player"))
        {
            Debug.Log("Aqu� entra");
            collisionObject.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collisionDetected)
    {
        GameObject collisionObject = collisionDetected.gameObject;
        if (collisionObject.CompareTag("Player"))
        {
            Debug.Log("Aqu� se va");
            collisionObject.transform.parent = null;
        }
    }
}
