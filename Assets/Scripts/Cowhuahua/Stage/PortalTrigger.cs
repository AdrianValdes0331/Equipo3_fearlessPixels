using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    public float xTarget, yTarget, zTarget;
    public AudioSource portalUsageSound;
    Vector3 targetPosition = new Vector3();

    private void OnTriggerEnter2D(Collider2D element) {
        if (!element.CompareTag("Driver") && !element.CompareTag("Untagged"))
        {
            targetPosition.x = xTarget;
            targetPosition.y = yTarget;
            targetPosition.z = zTarget;
            if (element.CompareTag("Player")){
                element.transform.parent.transform.position = targetPosition;
            }
            else {
                element.transform.position = targetPosition;
            }
            portalUsageSound.Play();
        }
    }
}
