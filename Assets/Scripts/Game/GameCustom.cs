using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCustom : MonoBehaviour
{

    public int players;
    public Text playersText;

    private void Update()
    {
        playersText.text = players.ToString();
    }

    public void Aumentar()
    {
        if (players < 4)
        {
            players++;
        }
        else
        {
            Debug.Log("No pasa nada :D");
        }
    }

    public void Disminuir()
    {
        if (players > 0)
        {
            players--;
        }
        else
        {
            Debug.Log("No pasa nada :D");
        }
        
    }

}
