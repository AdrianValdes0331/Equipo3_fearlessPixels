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
    public TextMeshProUGUI roomCodeText;


    public override void OnConnectedToMaster()
    {
        if (createRoomBtn != null && joinRoomBtn != null)
        {
            createRoomBtn.interactable = true;
            joinRoomBtn.interactable = true;
        }

        /*GetPlayerName();
        Debug.Log(PhotonNetwork.NickName);*/
    }

    public override void OnLeftRoom()
    {
        gameObject.GetComponent<CodeModal>().Cerrado();
        photonView.RPC("UpdatePlayerInfo", RpcTarget.All);
    }

    public void CreateOrJoinRoom()
    {
        if (PhotonNetwork.CurrentRoom != null)
        {
            ActivateLobbyWindow();
        } else
        {
            CreateRoom();
        }
       
    }

    public void JoinRoom(TMP_InputField _roomName)
    {
        Debug.Log("Conectado a room");
        Debug.Log(_roomName != null);
        NetworkManager.instance.JoinRoom(_roomName.text.ToUpper());
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("UpdatePlayerInfo", RpcTarget.All);
        }
    }

    public void CreateRoom()
    {
        NetworkManager.instance.CreateRoom(GetRoomCode());
    }

    public string GetRoomCode()
    {
        char first = (char)Random.Range('A', 'Z');
        char second = (char)Random.Range('A', 'Z');
        int third = Random.Range(0, 9);
        char fourth = (char)Random.Range('A', 'Z');
        int fifth = Random.Range(0, 9);

        return first.ToString() + second.ToString() + third.ToString() + fourth.ToString() + fifth.ToString();
    }


    public override void OnJoinedRoom()
    {

        if (PhotonNetwork.IsMasterClient)
        {
            ActivateLobbyWindow();
        }

        GetPlayerName();
        Debug.Log(PhotonNetwork.NickName);

        photonView.RPC("UpdatePlayerInfo", RpcTarget.All);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        photonView.RPC("UpdatePlayerInfo", RpcTarget.All);
    }

    public void GetPlayerName()
    {
        int playerNumber = PhotonNetwork.PlayerList.Length;
        Debug.Log(playerNumber);
        PhotonNetwork.NickName = "Player " + playerNumber.ToString();
    }

    private void ActivateLobbyWindow()
    {
        UpdateRoomCode();
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

    
    public void UpdateRoomCode()
    {
        roomCodeText.text = "Codigo: " + PhotonNetwork.CurrentRoom.Name;
    }

    public void LeaveLobby()
    {
        PhotonNetwork.LeaveRoom();
        //NetworkManager.instance.DestroyBeforeLeave();
        //Lobby.SetActive(false);
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        int stageIndex = PlayerPrefs.GetInt("StageIndex");
        Debug.Log(stageIndex);
        NetworkManager.instance.photonView.RPC("LoadScene", RpcTarget.All, SelectStage.Instance.stages[stageIndex].StageName); 
    }
}
