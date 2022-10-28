using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    public Transform Postition1;
    public Transform Postition2;
    public Transform Postition3;
    public Transform Postition4;
    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();     
        string sceneName = currentScene.name;
        int playerIndex = PlayerPrefs.GetInt("PlayerIndex");
        int player2Index = PlayerPrefs.GetInt("PlayerIndex2");
        int player3Index = PlayerPrefs.GetInt("PlayerIndex3");
        int player4Index = PlayerPrefs.GetInt("PlayerIndex4");
        if (sceneName != "Training")
        {
            Debug.Log(playerIndex);
            Debug.Log(player2Index);
            Debug.Log(player3Index);
            Debug.Log(player4Index);
            GameObject instance1 = Instantiate(SelectPlayers.Instance.players[playerIndex].playablePlayer, Postition1.position, Quaternion.identity);
            instance1.name = "Player1";
            GameObject instance2 = Instantiate(SelectPlayers.Instance.players[player2Index].playablePlayer, Postition2.position, Quaternion.identity);
            instance2.name = "Player2";
            GameObject instance3 = Instantiate(SelectPlayers.Instance.players[player3Index].playablePlayer, Postition3.position, Quaternion.identity);
            instance3.name = "Player3";
            GameObject instance4 = Instantiate(SelectPlayers.Instance.players[player4Index].playablePlayer, Postition4.position, Quaternion.identity);
            instance4.name = "Player4";
        }
        else
        {
            Instantiate(SelectPlayers.Instance.players[playerIndex].playablePlayer, Postition1.position, Quaternion.identity);
            Instantiate(SelectPlayers.Instance.players[5].playablePlayer, Postition2.position, Quaternion.identity);
        }
    }
}
