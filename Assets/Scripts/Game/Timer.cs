using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class Timer : MonoBehaviour
{
    public float TimeLeft;
    public bool TimerOn = false;
    public TextMeshProUGUI TimerTxt;
    public GameObject pauseMenu;
    public static bool isPaused;
    //public Transform Postition;

    // Start is called before the first frame update
    void Start()
    {
        TimerOn = true;       
        //var SPTimer = Instantiate(TimerTxt, Postition.position, Quaternion.identity);
        //SPTimer.transform.parent = gameObject.transform;
    }



    // Update is called once per frame
    void Update()
    {
        if (TimerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
            }
            else
            {
                Debug.Log("Time is UP!");
                TimeLeft = 0;
                TimerOn = false;
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                foreach (GameObject play in players)
                {
                    GameObject.Destroy(play);
                }

                //Destroy network manager
                PhotonNetwork.LeaveRoom();
                NetworkManager.instance.DestroyBeforeLeave();

                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
                isPaused = true;
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerTxt.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
