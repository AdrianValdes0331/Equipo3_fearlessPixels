using System.Collections.Generic;
using UnityEngine.Playables;
using System.Collections;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class LaunchFireBall : MonoBehaviour
{
    public GameObject FireBall, FireBallArrow;
    public InputActionReference actionReference;
    GameObject newArrow, newFireBall;

    private void Start()
    {
        actionReference.action.started += context =>
        {
            if (context.interaction is SlowTapInteraction)
            {
                InstantiateArrow();
            }
        };

        actionReference.action.canceled += context =>
        {
            if (context.interaction is SlowTapInteraction)
            {
                Destroy(newArrow);
            }
        };

        actionReference.action.performed += context =>
        {
            if (context.interaction is TapInteraction)
            {
                InstantiateFireBall(false);
            }
            else if (context.interaction is SlowTapInteraction)
            {
                InstantiateFireBall(true);
            }
        };
    }

    public void InstantiateArrow()
    {
        if (transform.localScale.x < 0)
        {
            newArrow = Instantiate(FireBallArrow, transform.position + Vector3.left, transform.rotation);
            Vector3 fixedScale = new Vector3(newArrow.transform.localScale.x * -1, newArrow.transform.localScale.y, newArrow.transform.localScale.z);
            newArrow.transform.localScale = fixedScale; 
        }
        else
        {
            newArrow = Instantiate(FireBallArrow, transform.position + Vector3.right, transform.rotation);
        }
        newArrow.transform.SetParent(transform);
    }

    public void InstantiateFireBall(bool withArrowDirection)
    {
        if (!withArrowDirection)
        {
            if (transform.localScale.x < 0)
            {
                Quaternion finalRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + 180);
                newFireBall = Instantiate(FireBall, transform.position + Vector3.left * 1.05f, finalRotation);
            }
            else
            {
                Instantiate(FireBall, transform.position + Vector3.right * 1.05f, transform.rotation);
            }
        }
        else
        {
            Vector3 arrowRotation = new Vector3(newArrow.transform.eulerAngles.x, newArrow.transform.eulerAngles.y, newArrow.transform.eulerAngles.z);
            if (transform.localScale.x < 0)
            {
                Quaternion finalRotation = Quaternion.Euler(arrowRotation.x, arrowRotation.y, arrowRotation.z + 180);
                newFireBall = Instantiate(FireBall, transform.position + Vector3.left * 1.05f, finalRotation);
            }
            else
            {
                Quaternion finalRotation = Quaternion.Euler(arrowRotation.x, arrowRotation.y, arrowRotation.z);
                newFireBall = Instantiate(FireBall, transform.position + Vector3.right * 1.05f, finalRotation);
            }
            Destroy(newArrow);
        }
    }
}
