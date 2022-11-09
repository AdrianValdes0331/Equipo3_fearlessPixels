using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class InitializeConnection : MonoBehaviourPunCallbacks
{

    [Header("Menu Principal")]
    public Button createRoomBtn;
    public Button joinRoomBtn;


    public override void OnConnectedToMaster()
    {
        createRoomBtn.interactable = true;
        joinRoomBtn.interactable = true;

        GetPlayerName();
        Debug.Log(PhotonNetwork.NickName);
    }

    public void GetPlayerName()
    {
        int playerNumber = PhotonNetwork.PlayerList.Length + 1;
        Debug.Log(playerNumber);
        PhotonNetwork.NickName = "Player " + playerNumber.ToString();
    }

    public void GetRoomCode(TMP_InputField _roomCode)
    {
        PlayerPrefs.SetString("RoomCode", _roomCode.text);
    }
}
