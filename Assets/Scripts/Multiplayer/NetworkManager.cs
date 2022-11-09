using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    //Singleton
    public static NetworkManager instance;

    private void Awake()
    {
        if (instance != null && instance != this) {
            gameObject.SetActive(false);
        } else {
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    //Conexión 
    public void CreateRoom(string _name)
    {
        PhotonNetwork.CreateRoom(_name); 
    }

    public override void OnCreatedRoom()
    {
        //base.OnCreatedRoom();
        Debug.Log("Se creo room: " + PhotonNetwork.CurrentRoom.Name);
    }

    public void JoinRoom(string _name)
    {
        PhotonNetwork.JoinRoom(_name);
    }

    [PunRPC]

    public void LoadScene(string _nameScene)
    {
        PhotonNetwork.LoadLevel(_nameScene); 
    }

    public string GetRoomCode()
    {

        return "";
    }
}
