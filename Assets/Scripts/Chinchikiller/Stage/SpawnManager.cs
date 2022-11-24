using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject PlayerManager;
    public GameObject MultiplayerManager;
    public GameObject TimerOnline;
    public GameObject TimerOffline;

    private void Awake()
    {
        int isOnline = PlayerPrefs.GetInt("isOnline");
        if (isOnline.Equals(1))
        {
            Destroy(PlayerManager);
            Destroy(TimerOffline);
        }
        else
        {
            Destroy(MultiplayerManager);
            Destroy(TimerOnline);
        }
    }
}
