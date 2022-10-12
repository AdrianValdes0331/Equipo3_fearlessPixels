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
            Debug.Log("Aquí entra");
            collisionObject.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collisionDetected)
    {
        GameObject collisionObject = collisionDetected.gameObject;
        if (collisionObject.CompareTag("Player"))
        {
            Debug.Log("Aquí se va");
            collisionObject.transform.parent = null;
        }
    }
}
