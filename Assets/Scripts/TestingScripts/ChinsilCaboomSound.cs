using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChinsilCaboomSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;
    public float volume = 1.0f;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Chinsil")
        {
            audioSource.PlayOneShot(clip, volume);
        }
    }
}
