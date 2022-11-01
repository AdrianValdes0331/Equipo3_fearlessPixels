using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class FightIntro : MonoBehaviour
{
    RawImage ready, fight;
    List<GameObject> Players = new List<GameObject>();
    Color fadedTextColor = new Color(1f, 1f, 1f, 0f);
    Color normalTextColor = new Color(1f, 1f, 1f, 1f);

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
        yield return new WaitForSeconds(0.21f);
        Players.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        for (int i = 0; i < Players.Count; i++)
        {
            PlayerInput currentPlayerInput = Players[i].GetComponent<PlayerInput>();
            if (currentPlayerInput)
            {
                currentPlayerInput.enabled = false;
            }
        }
    }

    IEnumerator PlayIntro()
    {
        yield return new WaitForSeconds(3f);
        ready.color = normalTextColor;
        yield return new WaitForSeconds(0.5f);
        ready.color = fadedTextColor;
        yield return new WaitForSeconds(0.15f);
        ready.color = normalTextColor;
        yield return new WaitForSeconds(0.5f);
        ready.color = fadedTextColor;
        yield return new WaitForSeconds(0.15f);
        ready.color = normalTextColor;
        yield return new WaitForSeconds(0.5f);
        ready.color = fadedTextColor;
        yield return new WaitForSeconds(0.15f);
        ready.color = normalTextColor;
        yield return new WaitForSeconds(0.5f);
        ready.color = fadedTextColor;
        fight.color = normalTextColor;
        yield return new WaitForSeconds(0.25f);
        fight.color = fadedTextColor;
        yield return new WaitForSeconds(0.05f);
        fight.color = normalTextColor;
        yield return new WaitForSeconds(0.25f);
        fight.color = fadedTextColor;
        yield return new WaitForSeconds(0.05f);
        fight.color = normalTextColor;
        yield return new WaitForSeconds(0.25f);
        fight.color = fadedTextColor;
        for (int i = 0; i < Players.Count; i++)
        {
            PlayerInput currentPlayerInput = Players[i].GetComponent<PlayerInput>();
            if (currentPlayerInput)
            {
                currentPlayerInput.enabled = true;
            }
        }
    }
}
