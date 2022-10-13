using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    public float xTarget, yTarget, zTarget;
    public AudioSource portalUsageSound;
    Vector3 targetPosition = new Vector3();

    private void OnTriggerEnter2D(Collider2D element) {
        //if (element.CompareTag("Player")){
            targetPosition.x = xTarget;
            targetPosition.y = yTarget;
            targetPosition.z = zTarget;
            element.transform.position = targetPosition;
            portalUsageSound.Play();
        //}
    }
}
