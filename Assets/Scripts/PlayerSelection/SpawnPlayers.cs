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
        Instantiate(SelectPlayers.Instance.players[playerIndex].playablePlayer, Postition1.position, Quaternion.identity);
        try
        {
            //Instantiate dummy in training area
            Instantiate(SelectPlayers.Instance.players[2].playablePlayer, Postition2.position, Quaternion.identity);
        }
        catch {}
    }
}
