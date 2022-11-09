using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class MenuUIController : MonoBehaviourPunCallbacks
{

    public GameObject Lobby;
    public GameObject MainWindow;

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

        GetPlayerName();
        Debug.Log(PhotonNetwork.NickName);
    }

    public void CreateOrJoinRoom()
    {
        int type = PlayerPrefs.GetInt("MultiplayerType");
        if(type.Equals(0)){
            CreateRoom();
        } else {
            string code = PlayerPrefs.GetString("RoomCode");
            JoinRoom(code);
        }
    }

    public void JoinRoom(string _roomName)
    {

        NetworkManager.instance.JoinRoom(_roomName);
        photonView.RPC("UpdatePlayerInfo", RpcTarget.All);
    }

    public void CreateRoom()
    {
        NetworkManager.instance.CreateRoom("test");
    }


    public override void OnJoinedRoom()
    {
        ActivateLobbyWindow();

        photonView.RPC("UpdatePlayerInfo", RpcTarget.All);
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

    private void ActivateLobbyWindow()
    {
        Lobby.SetActive(true);
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
        Time.timeScale = 1f;
        int stageIndex = PlayerPrefs.GetInt("StageIndex");
        Debug.Log(stageIndex);
        NetworkManager.instance.photonView.RPC("LoadScene", RpcTarget.All, SelectStage.Instance.stages[stageIndex].StageName); 
    }
}
