using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioSettings : MonoBehaviour
{
    VolumeSlider a;

    public static AudioSettings instance;

    private void Awake()
    {

        DontDestroyOnLoad(this.gameObject);

        AudioListener.volume = PlayerPrefs.GetFloat("musicVolume");

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


}
