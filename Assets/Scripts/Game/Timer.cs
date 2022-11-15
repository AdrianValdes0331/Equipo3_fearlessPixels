using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float TimeLeft;
    public TextMeshProUGUI TimerTxt;
    public GameObject pauseMenu;
    public static bool isPaused;
    [HideInInspector] public bool TimerOn = false;
    FightIntroEnding introEndingScript;
    //public Transform Postition;

    void Start()
    {
        TimeLeft -= 1;
        updateTimer(TimeLeft);
        introEndingScript = GameObject.Find("Intro&EndingManager").GetComponent<FightIntroEnding>();
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
                introEndingScript.CheckForWinnerWhenTimeUp();
            }
        }
    }

    public void ReturnToMainMenu()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject play in players)
        {
            GameObject.Destroy(play);
        }
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        SceneManager.LoadScene("MainMenu");
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerTxt.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
