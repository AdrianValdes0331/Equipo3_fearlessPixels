using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkLight : MonoBehaviour
{
    new Light light;
    bool timerOn = false;
    public float waitSeconds;
    public bool produceSound;
    public AudioSource blinkLightSound;

    void Start()
    {
        light = GetComponent<Light>();
    }

    void Update()
    {
        if (!timerOn)
        {
            StartCoroutine(blinkLight());
        }
    }

    IEnumerator blinkLight()
    {
        timerOn = true;
        yield return new WaitForSeconds(waitSeconds);
        if (produceSound)
        {
            blinkLightSound.Play();
        }
        light.enabled = false;
        yield return new WaitForSeconds(0.4f);
        light.enabled = true;
        yield return new WaitForSeconds(0.2f);
        light.enabled = false;
        yield return new WaitForSeconds(0.2f);
        light.enabled = true;
        yield return new WaitForSeconds(0.1f);
        light.enabled = false;
        yield return new WaitForSeconds(0.1f);
        light.enabled = true;
        timerOn = false;
    }
}
