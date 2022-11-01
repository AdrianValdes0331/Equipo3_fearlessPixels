using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class FightIntro : MonoBehaviour
{
    RawImage ready, fight;
    List<GameObject> Drivers = new List<GameObject>();
    Color fadedTextColor = new Color(1f, 1f, 1f, 0f);
    Color normalTextColor = new Color(1f, 1f, 1f, 1f);
    bool readyEnded = false;
    bool fightEnded = false;
    bool introEnded = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForPlayers());
        ready = GameObject.Find("Main Camera/Canvas/Ready?").GetComponent<RawImage>();
        fight = GameObject.Find("Main Camera/Canvas/Fight!").GetComponent<RawImage>();
        ready.color = fadedTextColor;
        fight.color = fadedTextColor;
        StartCoroutine(PlayIntro());
    }

    IEnumerator WaitForPlayers()
    {
        yield return new WaitForSeconds(0.205f);
        Drivers.AddRange(GameObject.FindGameObjectsWithTag("Driver"));
        for (int i = 0; i < Drivers.Count; i++)
        {
            PlayerInput currentPlayerInput = Drivers[i].GetComponent<PlayerInput>();
            if (currentPlayerInput)
            {
                currentPlayerInput.enabled = false;
            }
        }
    }

    IEnumerator FadeText(RawImage textImage, float normalDuration, float fastDuration, int numberOfTimes, bool ended)
    {
        for (int i = 0; i < numberOfTimes; i ++)
        {
            textImage.color = normalTextColor;
            yield return new WaitForSeconds(normalDuration);
            textImage.color = fadedTextColor;
            yield return new WaitForSeconds(fastDuration);
        }
        ended = true;
    }

    IEnumerator PlayIntro()
    {
        float normalReadyDuration = 0.5f;
        float fastReadyDuration = 0.1f;
        float normalFightDuration = 0.25f;
        float fastFightDuration = 0.075f;
        int readyTimes = 4;
        int fightTimes = 5;
        yield return new WaitForSeconds(3f);
        StartCoroutine(FadeText(ready, normalReadyDuration, fastReadyDuration, readyTimes, readyEnded));
        yield return new WaitForSeconds(readyTimes * (normalReadyDuration + fastReadyDuration) + 0.1f);
        StartCoroutine(FadeText(fight, normalFightDuration, fastFightDuration, fightTimes, fightEnded));
        yield return new WaitForSeconds(fightTimes * (normalFightDuration + fastFightDuration) + 0.1f);
        for (int i = 0; i < Drivers.Count; i++)
        {
            PlayerInput currentPlayerInput = Drivers[i].GetComponent<PlayerInput>();
            if (currentPlayerInput)
            {
                currentPlayerInput.enabled = true;
            }
        }
    }
}
