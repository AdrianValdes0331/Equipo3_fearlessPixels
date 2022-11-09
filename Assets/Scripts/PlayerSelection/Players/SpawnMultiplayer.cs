using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class SpawnMultiplayer : MonoBehaviourPunCallbacks
{
    //Instancia
    public static SpawnMultiplayer instance;

    public bool isGameEnd = false; //Saber si el juego se completo

    public PlayerController[] players; //controlador de player

    private int playerInGame; //Numero de players en el room

    public Transform Postition1;
    public Transform Postition2;
    public Transform Postition3;
    public Transform Postition4;

    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        players = new PlayerController[PhotonNetwork.PlayerList.Length]; // Inicializar el vector de jugadores
        photonView.RPC("InGame", RpcTarget.AllBuffered);// Colocar los players en una posicion de lista de spawner
    }


    [PunRPC]
    void InGame()
    {

        playerInGame++; // contador de jugadores
        if (playerInGame == PhotonNetwork.PlayerList.Length)
        {
            SpawnPlayer();// mandar llamar posicionamiento de player
        }
    }

    void SpawnPlayer()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        int playerIndex = PlayerPrefs.GetInt("PlayerIndex");
        int player2Index = PlayerPrefs.GetInt("PlayerIndex2");
        int player3Index = PlayerPrefs.GetInt("PlayerIndex3");
        int player4Index = PlayerPrefs.GetInt("PlayerIndex4");

        Debug.Log(playerIndex);
        Debug.Log(player2Index);
        Debug.Log(player3Index);
        Debug.Log(player4Index);
        GameObject instance1 = PhotonNetwork.Instantiate(SelectPlayers.Instance.players[playerIndex].playablePlayer.name, Postition1.position, Quaternion.identity);
        instance1.name = "Player1";
        GameObject instance2 = PhotonNetwork.Instantiate(SelectPlayers.Instance.players[player2Index].playablePlayer.name, Postition2.position, Quaternion.identity);
        instance2.name = "Player2";
        GameObject instance3 = PhotonNetwork.Instantiate(SelectPlayers.Instance.players[player3Index].playablePlayer.name, Postition3.position, Quaternion.identity);
        instance3.name = "Player3";
        GameObject instance4 = PhotonNetwork.Instantiate(SelectPlayers.Instance.players[player4Index].playablePlayer.name, Postition4.position, Quaternion.identity);
        instance4.name = "Player4";

    }
}
