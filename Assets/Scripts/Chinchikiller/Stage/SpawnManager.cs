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

    public GameObject[] playersDMG;

    private void Awake()
    {
        int isOnline = PlayerPrefs.GetInt("isOnline");
        if (isOnline.Equals(1))
        {
            Destroy(PlayerManager);
            Destroy(TimerOffline);
            Canvas.GetComponent<TagFinder>().enabled = false;
            Canvas.GetComponent<PlayerDmg>().enabled = false;
            Canvas.GetComponent<MultiplayerDmg>().enabled = true;
        }
        else
        {
            Destroy(MultiplayerManager);
            Destroy(TimerOnline);
            Canvas.GetComponent<TagFinder>().enabled = true;
            Canvas.GetComponent<PlayerDmg>().enabled = true;
            Canvas.GetComponent<MultiplayerDmg>().enabled = false;

            for (int i = 0; i < playersDMG.Length; i++)
            {
                playersDMG[i].SetActive(false);
            }
        }
    }
}
