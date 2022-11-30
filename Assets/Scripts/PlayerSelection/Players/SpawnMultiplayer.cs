using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;

public class SpawnMultiplayer : MonoBehaviourPunCallbacks
{
    //Instancia
    public static SpawnMultiplayer instance;

    public bool isGameEnd = false; //Saber si el juego se completo

    public MultiplayerController[] players; //controlador de player

    private int playerInGame; //Numero de players en el room

    public Transform Position1;
    public Transform Position2;
    public Transform Position3;
    public Transform Position4;

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

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        object[] instantiationData = info.photonView.InstantiationData;
        // ...
    }

    void SpawnPlayer()
    {
        int playerIndex = PlayerPrefs.GetInt("PlayerIndex");

        // Asignar el nombre correcto dependiendo del numero de player
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

        // Obtener una posición de spawn random
        int positionIndex = Random.Range(1, 5);
        Vector3 playerPosition = Position1.position;
        if(positionIndex == 2)
        {
            playerPosition = Position2.position;
        } else if (positionIndex == 3)
        {
            playerPosition = Position3.position;
        } else if (positionIndex == 4)
        {
            playerPosition = Position4.position;
        }

        Debug.Log(playerPosition);
        Debug.Log(playerIndex);

        //object[] myCustomInitData = GetInitData();
        GameObject instance1 = PhotonNetwork.Instantiate(SelectPlayers.Instance.players[playerIndex].playablePlayer.name, playerPosition, Quaternion.identity);
        instance1.name = playerName;

        MultiplayerController playScript = instance1.GetComponent<MultiplayerController>();// Obtener script que controla al jugador
        playScript.photonView.RPC("Init", RpcTarget.All, PhotonNetwork.LocalPlayer); // Mandar ejecutar funcion de inicializador de player

    }
  
}
