using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject PlayerManager;
    public GameObject MultiplayerManager;
    public GameObject TimerOnline;
    public GameObject TimerOffline;
    public GameObject Canvas;

    private void Awake()
    {
        int isOnline = PlayerPrefs.GetInt("isOnline");
        if (isOnline.Equals(1))
        {
            Destroy(PlayerManager);
            Destroy(TimerOffline);
            Canvas.GetComponent<TagFinder>().enabled = false;
            Canvas.GetComponent<TagFinderOnline>().enabled = true;
        }
        else
        {
            Destroy(MultiplayerManager);
            Destroy(TimerOnline);
            Canvas.GetComponent<TagFinder>().enabled = true;
            Canvas.GetComponent<TagFinderOnline>().enabled = false;
        }
    }
}
