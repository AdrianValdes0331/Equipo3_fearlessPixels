using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingSound : MonoBehaviour
{
    public AudioSource audioSource;
    [HideInInspector] public Animator Animator;
    public AudioClip clip;
    public float volume = 1.0f;
    public string AnimWalk = "none";
    public NewMovement GMove;

    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    void Update() 
    {
        if (GMove.i_movement.x != 0.0f) {
            audioSource.PlayOneShot(clip, volume);
        }
    }  
}
