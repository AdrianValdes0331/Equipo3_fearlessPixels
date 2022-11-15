using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class FightIntroEnding : MonoBehaviour
{
    RawImage ready, fight, winner, tie;
    List<GameObject> Drivers = new List<GameObject>();
    List<GameObject> Players = new List<GameObject>();
    Color fadedTextColor = new Color(1f, 1f, 1f, 0f);
    Color normalTextColor = new Color(1f, 1f, 1f, 1f);
    DynamicCamera cameraScript;
    Timer timerScript;
    AudioSource winSound, win2Sound, win3Sound, tieSound;
    float normalReadyDuration = 0.5f;
    float fastReadyDuration = 0.1f;
    float normalFightDuration = 0.25f;
    float fastFightDuration = 0.075f;
    float normalWinnerDuration = 0.5f;
    float fastWinnerDuration = 0.1f;
    int readyTimes = 4;
    int fightTimes = 5;
    int winnerTimes = 7;
    int livesNumber;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForPlayers());
        ready = GameObject.Find("Main Camera/Canvas/Ready?").GetComponent<RawImage>();
        fight = GameObject.Find("Main Camera/Canvas/Fight!").GetComponent<RawImage>();
        winner = GameObject.Find("Main Camera/Canvas/Winner!").GetComponent<RawImage>();
        tie = GameObject.Find("Main Camera/Canvas/Tie").GetComponent<RawImage>();
        cameraScript = GameObject.Find("Main Camera").GetComponent<DynamicCamera>();
        timerScript = GameObject.Find("Timer").GetComponent<Timer>();
        winSound = GameObject.Find("Scenery/Sounds/Win").GetComponent<AudioSource>();
        win2Sound = GameObject.Find("Scenery/Sounds/Win2").GetComponent<AudioSource>();
        win3Sound = GameObject.Find("Scenery/Sounds/Win3").GetComponent<AudioSource>();
        tieSound = GameObject.Find("Scenery/Sounds/Tie").GetComponent<AudioSource>();
        ready.color = fadedTextColor;
        fight.color = fadedTextColor;
        winner.color = fadedTextColor;
        StartCoroutine(PlayIntro());
    }

    IEnumerator WaitForPlayers()
    {
        yield return new WaitForSeconds(0.205f);
        Drivers.AddRange(GameObject.FindGameObjectsWithTag("Driver"));
        Players.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        for (int i = 0; i < Drivers.Count; i++)
        {
            PlayerInput currentPlayerInput = Drivers[i].GetComponent<PlayerInput>();
            if (currentPlayerInput)
            {
                currentPlayerInput.enabled = false;
            }
        }
        FreezePlayers(Players);
    }

    public void CheckForWinner()
    {
        List<GameObject> AlivedPlayers = new List<GameObject>();
        AlivedPlayers.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        if (AlivedPlayers.Count - 1 == 1)
        {
            timerScript.TimerOn = false;
            FreezePlayers(AlivedPlayers);
            cameraScript.FocusWinner(null);
            StartCoroutine(PlayEnding(true));
        }
    }

    public void CheckForWinnerWhenTimeUp()
    {
        List<GameObject> AlivedPlayers = new List<GameObject>();
        AlivedPlayers.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        FreezePlayers(AlivedPlayers);
        livesNumber = AlivedPlayers[0].GetComponent<Respawn>().startingNumberLives;
        int[] livesOfPlayers = new int[AlivedPlayers.Count];
        int[] livesCount = new int[livesNumber];
        for (int i = 0; i < AlivedPlayers.Count; i++)
        {
            int currentPlayerLives = AlivedPlayers[i].GetComponent<Respawn>().lives;
            livesOfPlayers[i] = currentPlayerLives;
            livesCount[currentPlayerLives - 1]++;
        }

        for (int i = livesNumber - 1; i >= 0; i--)
        {
            if (livesCount[i] == 1)
            {
                cameraScript.FocusWinner(AlivedPlayers[System.Array.IndexOf(livesOfPlayers, i + 1)]);
                StartCoroutine(PlayEnding(true));
                return;
            }
        }
        StartCoroutine(PlayEnding(false));
    }

    public void FreezePlayers(List<GameObject> AlivedPlayers)
    {
        for (int i = 0; i < AlivedPlayers.Count; i++)
        {
            PlayerInput currentPlayerInput = AlivedPlayers[i].GetComponent<PlayerInput>();
            if (currentPlayerInput)
            {
                currentPlayerInput.enabled = false;
            }
            else
            {
                Rigidbody2D playerRigidbody = AlivedPlayers[i].GetComponent<Rigidbody2D>();
                playerRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }

    public void PlayEndingSound(bool victory)
    {
        if (victory)
        {
            int randomVictorySound = Random.Range(0, 3);
            switch (randomVictorySound)
            {
                case 0:
                    winSound.Play();
                    break;
                case 1:
                    win2Sound.Play();
                    break;
                case 2:
                    win3Sound.Play();
                    break;
            }
        }
        else
        {
            tieSound.Play();
        }
    }

    public IEnumerator PlayEnding(bool thereIsWinner)
    {
        if (thereIsWinner)
        {
            StartCoroutine(FadeImage(winner, normalWinnerDuration, fastWinnerDuration, winnerTimes));
            PlayEndingSound(true);
            yield return new WaitForSeconds(winnerTimes * (normalWinnerDuration + fastWinnerDuration));
            winner.color = normalTextColor;
        }
        else
        {
            StartCoroutine(FadeImage(tie, normalWinnerDuration, fastWinnerDuration, winnerTimes));
            PlayEndingSound(false);
            yield return new WaitForSeconds(winnerTimes * (normalWinnerDuration + fastWinnerDuration));
            tie.color = normalTextColor;
        }
        yield return new WaitForSeconds(3f);
        timerScript.ReturnToMainMenu();
    }

    IEnumerator FadeImage(RawImage textImage, float normalDuration, float fastDuration, int numberOfTimes)
    {
        for (int i = 0; i < numberOfTimes; i ++)
        {
            textImage.color = normalTextColor;
            yield return new WaitForSeconds(normalDuration);
            textImage.color = fadedTextColor;
            yield return new WaitForSeconds(fastDuration);
        }
    }

    IEnumerator PlayIntro()
    {
        yield return new WaitForSeconds(3f);
        StartCoroutine(FadeImage(ready, normalReadyDuration, fastReadyDuration, readyTimes));
        yield return new WaitForSeconds(readyTimes * (normalReadyDuration + fastReadyDuration) + 0.1f);
        StartCoroutine(FadeImage(fight, normalFightDuration, fastFightDuration, fightTimes));
        yield return new WaitForSeconds(fightTimes * (normalFightDuration + fastFightDuration) + 0.1f);
        timerScript.TimerOn = true;
        for (int i = 0; i < Drivers.Count; i++)
        {
            PlayerInput currentPlayerInput = Drivers[i].GetComponent<PlayerInput>();
            if (currentPlayerInput)
            {
                currentPlayerInput.enabled = true;
            }
        }
        for (int i = 0; i < Players.Count; i++)
        {
            PlayerInput currentPlayerInput = Players[i].GetComponent<PlayerInput>();
            if (currentPlayerInput)
            {
                currentPlayerInput.enabled = true;
            }
            else
            {
                Rigidbody2D playerRigidbody = Players[i].GetComponent<Rigidbody2D>();
                playerRigidbody.constraints &= ~RigidbodyConstraints2D.FreezeAll;
            }
        }
    }
}
