using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class MultiplayerDmgPercent : MonoBehaviourPunCallbacks
{

    public TextMeshProUGUI dmgPlayer;
    public GameObject DmgManager;
    public DmgManager DmgManagerScript;
    public GameObject vidas;

    // Start is called before the first frame update
    void Start()
    {
        DmgManager = GameObject.Find("DmgManager");
        DmgManagerScript = DmgManager.GetComponent<DmgManager>();
        string nickname = PhotonNetwork.NickName;

        //dmgPlayer = DmgManager.GetComponent<DmgManager>().dmgPlayer1;
        //vidas = DmgManager.GetComponent<DmgManager>().vidasPlayer1;


        if (nickname.Equals("Player 1"))
        {
            DmgManagerScript.updateDmgPercentTxt("10%", "P1");
        } else if (nickname.Equals("Player 2"))
        {
            DmgManagerScript.updateDmgPercentTxt("20%", "P2");
            //dmgPlayer = DmgManager.GetComponent<DmgManager>().dmgPlayer2;
            //vidas = DmgManager.GetComponent<DmgManager>().vidasPlayer2;
        }
        else if (nickname.Equals("Player 3"))
        {
            DmgManagerScript.updateDmgPercentTxt("30%", "P3");
            //dmgPlayer = DmgManager.GetComponent<DmgManager>().dmgPlayer3;
            //vidas = DmgManager.GetComponent<DmgManager>().vidasPlayer3;
        }
        else if (nickname.Equals("Player 4"))
        {
            DmgManagerScript.updateDmgPercentTxt("40%", "P4");
            dmgPlayer = DmgManager.GetComponent<DmgManager>().dmgPlayer4;
            //vidas = DmgManager.GetComponent<DmgManager>().vidasPlayer4;
        }

        //dmgPlayer.text = "10%";
    }
}
