using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    public Transform Postition1;
    private void Start()
    {
        int playerIndex = PlayerPrefs.GetInt("PlayerIndex");
        Instantiate(SelectPlayers.Instance.players[playerIndex].playablePlayer, Postition1.position, Quaternion.identity);
    }
}
