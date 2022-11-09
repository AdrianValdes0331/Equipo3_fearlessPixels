using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class MenuUIController : MonoBehaviourPunCallbacks 
{


    public GameObject JoinWindow;
    public GameObject CreateWindow;
    public GameObject Lobby;

    [Header("Menu Principal")] 
    public Button createRoomBtn;
    public Button joinRoomBtn;

    [Header("Lobby")]
    public Button StartGameBtn;
    public TextMeshProUGUI playertextList;

    public override void OnConnectedToMaster()
    {
        createRoomBtn.interactable = true;
        joinRoomBtn.interactable = true;
    }

    public void JoinRoom(TMP_InputField _roomName)
    {
        NetworkManager.instance.JoinRoom(_roomName.text);
    }

    public void CreateRoom()
    {
        NetworkManager.instance.CreateRoom("test");
    }

    public void GetPlayerName()
    {
        int playerNumber = PhotonNetwork.PlayerList.Length + 1;
        PhotonNetwork.NickName = "Player " + playerNumber.ToString();
    }

    public override void OnJoinedRoom()
    {
        Lobby.SetActive(true);
        JoinWindow.SetActive(false);
        CreateWindow.SetActive(false);

    }

    [PunRPC]
    public void UpdatePlayerInfo()
    {
        playertextList.text = "";
        foreach(Player player in PhotonNetwork.PlayerList)
        {
            playertextList.text += player.NickName + "\n";
        }

        if (PhotonNetwork.IsMasterClient)
        {
            StartGameBtn.interactable = true;
        }
        else
        {
            StartGameBtn.interactable = false;
        }
    }

    public void LeaveLobby()
    {
        PhotonNetwork.LeaveRoom();
        Lobby.SetActive(false);
    }

    public void StartGame()
    {
        NetworkManager.instance.photonView.RPC("LoadScene", RpcTarget.All, "Training"); 
    }
}
