using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class TagFinderOnline : MonoBehaviourPunCallbacks
{
    private int playerCount;
    private GameObject[] players;
    private IEnumerator coroutine;
    public GameObject P1;
    public GameObject P2;
    public GameObject P3;
    public GameObject P4;
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            coroutine = WaitAndPrint(0.5f);
            StartCoroutine(coroutine);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    [PunRPC]
    void runScript()
    {
        int k = 0;
        //PhotonNetwork.LocalPlayer.
        string playerNickname = PhotonNetwork.LocalPlayer.NickName;
        GameObject playerr = GameObject.Find("Player1");
        string CurrPlayer = P1.name;
        string instanceName = "P1Tag";

        if (playerNickname.Equals("Player 2")){
            playerr = GameObject.Find("Player2");
            CurrPlayer = P2.name;
            instanceName = "P2Tag";
        } else if(playerNickname.Equals("Player 3"))
        {
            playerr = GameObject.Find("Player3");
            CurrPlayer = P3.name;
            instanceName = "P3Tag";
        } else if (playerNickname.Equals("Player 4"))
        {
            playerr = GameObject.Find("Player4");
            CurrPlayer = P4.name;
            instanceName = "P4Tag";
        }

        if (playerr.transform.childCount > 0)
        {
            Transform p1name = playerr.transform.GetChild(0).Find("Tag");
            Debug.Log(p1name);
            GameObject instance = PhotonNetwork.Instantiate(CurrPlayer, p1name.transform.position, transform.rotation);
                //Instantiate(CurrPlayer, p1name.transform.position, transform.rotation);
            instance.transform.parent = p1name.transform;
            instance.name = instanceName;
        }


    }

    private IEnumerator WaitAndPrint(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        //photonView.RPC("runScript", RpcTarget.All);
    }
}
