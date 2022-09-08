using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeG : MonoBehaviour
{
    public int timeG;
    public Text timeText;

    private void Update()
    {
        timeText.text = timeG.ToString();
    }

    public void Aumentar()
    {
        if (timeG < 10)
        {
            timeG++;
        }
        else
        {
            Debug.Log("No pasa nada :D");
        }
    }

    public void Disminuir()
    {
        if (timeG > 3)
        {
            timeG--;
        }
        else
        {
            Debug.Log("No pasa nada :D");
        }

    }
}
