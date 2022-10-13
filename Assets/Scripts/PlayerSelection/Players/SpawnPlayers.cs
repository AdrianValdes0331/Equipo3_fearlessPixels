using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    public Transform[] positions;
    //public Transform Postition2;
    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();     
        string sceneName = currentScene.name;
        int playerIndex = PlayerPrefs.GetInt("PlayerIndex");
        int player2Index = PlayerPrefs.GetInt("PlayerIndex2");
        if (sceneName != "Training")
        {
            Instantiate(SelectPlayers.Instance.players[playerIndex].playablePlayer, Postition1.position, Quaternion.identity);
            Instantiate(SelectPlayers.Instance.players[player2Index].playablePlayer, Postition2.position, Quaternion.identity);
        }
        else
        {
            for(int i = 0; i<SelectPlayers.Instance.players.Count; i++){}
            //Instantiate(SelectPlayers.Instance.players[2].playablePlayer, Postition2.position, Quaternion.identity);
        }

        /*try
        {
            //Instantiate dummy in training area
            Instantiate(SelectPlayers.Instance.players[2].playablePlayer, Postition2.position, Quaternion.identity);
        }
        catch { }*/
        
    }
}
