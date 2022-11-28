using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerOnline : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI TimerTxt;

    [HideInInspector] public bool startTimer = false;

    double timeLeft;
    double startTime;
    [SerializeField] float timer = 20f;
    ExitGames.Client.Photon.Hashtable CustomeValue;

    FightIntroEnding introEndingScript;

    void Start()
    {
        introEndingScript = GameObject.Find("Intro&EndingManager").GetComponent<FightIntroEnding>();
        if (PhotonNetwork.IsMasterClient)
        {
            CustomeValue = new ExitGames.Client.Photon.Hashtable();
            startTime = PhotonNetwork.Time;
            CustomeValue.Add("StartTime", startTime);
            PhotonNetwork.CurrentRoom.SetCustomProperties(CustomeValue);
        }
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            startTime = (double)(PhotonNetwork.CurrentRoom.CustomProperties["StartTime"]);
        }
    }

    void Update()
    {
        if (!startTimer) return;
        timeLeft = PhotonNetwork.Time - startTime;
        updateTimer(((float)timeLeft));
        if (timeLeft >= timer)
        {
            startTimer = false;
            timeLeft = 0;
            Debug.Log("Time is UP!");

            introEndingScript.CheckForWinnerWhenTimeUp();

        }
    }

    public void ReturnToMainMenu()
    {
        //Destroy network manager
        PhotonNetwork.LeaveRoom();
        NetworkManager.instance.DestroyBeforeLeave();

        SceneManager.LoadScene("MainMenu");
    }

    void updateTimer(float currentTime)
    {
        currentTime -= 1;
        currentTime = timer - currentTime;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerTxt.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
