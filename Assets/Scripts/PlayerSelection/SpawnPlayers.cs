using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    public Transform Postition1;
    public Transform Postition2;
    private void Start()
    {
        int playerIndex = PlayerPrefs.GetInt("PlayerIndex");
        int player2Index = PlayerPrefs.GetInt("PlayerIndex2");
        Instantiate(SelectPlayers.Instance.players[playerIndex].playablePlayer, Postition1.position, Quaternion.identity);
        Instantiate(SelectPlayers.Instance.players[player2Index].playablePlayer, Postition2.position, Quaternion.identity);
        
        if (player2Index == null)
        {
            try
            {
                //Instantiate dummy in training area
                Instantiate(SelectPlayers.Instance.players[2].playablePlayer, Postition2.position, Quaternion.identity);
            }
            catch { }
        }
    }
}
