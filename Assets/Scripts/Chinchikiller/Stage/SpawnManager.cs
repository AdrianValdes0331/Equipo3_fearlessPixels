using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject PlayerManager;
    public GameObject MultiplayerManager;

    private void Awake()
    {
        int isOnline = PlayerPrefs.GetInt("isOnline");
        if (isOnline.Equals(1))
        {
            Destroy(PlayerManager);
        }
        else
        {
            Destroy(MultiplayerManager);
        }
    }
}
