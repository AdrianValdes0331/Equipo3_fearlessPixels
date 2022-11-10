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

    public MultiplayerController[] players; //controlador de player

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
        players = new MultiplayerController[PhotonNetwork.PlayerList.Length]; // Inicializar el vector de jugadores
        photonView.RPC("InGame", RpcTarget.AllBuffered);// Colocar los players en una posicion de lista de spawner
    }


    [PunRPC]
    void InGame()
    {
        Debug.Log("InGame");
        playerInGame++; // contador de jugadores
        if (playerInGame == PhotonNetwork.PlayerList.Length)
        {
            Debug.Log("spawwnnn");
            SpawnPlayer();// mandar llamar posicionamiento de player
        }
    }

    void SpawnPlayer()
    {
        int playerIndex = PlayerPrefs.GetInt("PlayerIndex");
        /*int player2Index = PlayerPrefs.GetInt("PlayerIndex2");
        int player3Index = PlayerPrefs.GetInt("PlayerIndex3");
        int player4Index = PlayerPrefs.GetInt("PlayerIndex4");*/
        string playerName = "Player1";
        if (PhotonNetwork.NickName.Equals("Player 2"))
        {
            playerName = "Player2";
        }
        else if (PhotonNetwork.NickName.Equals("Player 3"))
        {
            playerName = "Player3";
        }
        else if (PhotonNetwork.NickName.Equals("Player 4"))
        {
            playerName = "Player4";
        }

        Debug.Log(playerIndex);
        /*Debug.Log(player2Index);
        Debug.Log(player3Index);
        Debug.Log(player4Index);*/
        GameObject instance1 = PhotonNetwork.Instantiate(SelectPlayers.Instance.players[playerIndex].playablePlayer.name, Postition1.position, Quaternion.identity);
        instance1.name = playerName;
        /*GameObject instance2 = PhotonNetwork.Instantiate(SelectPlayers.Instance.players[player2Index].playablePlayer.name, Postition2.position, Quaternion.identity);
        instance2.name = "Player2";
        GameObject instance3 = PhotonNetwork.Instantiate(SelectPlayers.Instance.players[player3Index].playablePlayer.name, Postition3.position, Quaternion.identity);
        instance3.name = "Player3";
        GameObject instance4 = PhotonNetwork.Instantiate(SelectPlayers.Instance.players[player4Index].playablePlayer.name, Postition4.position, Quaternion.identity);
        instance4.name = "Player4";*/


        Debug.Log(instance1.transform.GetChild(0));
        MultiplayerController playScript = instance1.transform.GetChild(0).GetComponent<MultiplayerController>();// Obtener script que controla al jugador
        playScript.photonView.RPC("Init", RpcTarget.All, PhotonNetwork.LocalPlayer); // Mandar ejecutar funcion de inicializador de player

    }
}
