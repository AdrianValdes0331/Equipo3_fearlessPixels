using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftPath : MonoBehaviour
{
    public Vector3[] currentPoint = new Vector3[4];
    int targetIndex = 0;
    bool goingUp = true;
    bool liftStopped = true;
    float lowerSpeed, currentSpeed;
    public float speed;
    public AudioSource liftStartSound, liftStopSound;

    void Start()
    {
        lowerSpeed = speed * 0.55f;
        StartCoroutine(startWait());
    }

    void Update()
    {
        if (!liftStopped)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentPoint[targetIndex], Time.deltaTime * currentSpeed);
            if (transform.position == currentPoint[targetIndex])
            {
                if (goingUp)
                {
                    if (targetIndex == 0 || targetIndex == 2)
                    {
                        currentSpeed = lowerSpeed;
                    }
                    else
                    {
                        currentSpeed = speed;
                    }

                    if (targetIndex != 3)
                    {
                        targetIndex++;
                    }
                    else
                    {
                        goingUp = false;
                        StartCoroutine(stopLiftTemporarily());
                    }
                }
                else
                {
                    if (targetIndex == 1 || targetIndex == 3)
                    {
                        currentSpeed = lowerSpeed;
                    }
                    else
                    {
                        currentSpeed = speed;
                    }

                    if (targetIndex != 0)
                    {
                        targetIndex--;
                    }
                    else
                    {
                        goingUp = true;
                        StartCoroutine(stopLiftTemporarily());
                    }
                }
            }
        }
    }

    IEnumerator stopLiftTemporarily()
    {
        liftStopped = true;
        liftStartSound.Stop();
        liftStopSound.Play();
        currentSpeed = 0;
        yield return new WaitForSeconds(4);
        liftStartSound.Play();
        liftStopped = false;
    }

    IEnumerator startWait()
    {
        liftStopped = true;
        currentSpeed = 0;
        yield return new WaitForSeconds(4);
        liftStartSound.Play();
        liftStopped = false;
    }
}
