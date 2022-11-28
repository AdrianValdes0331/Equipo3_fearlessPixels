using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class MultiplayerDmgPercent : MonoBehaviour
{

    public TextMeshProUGUI dmgPlayer;
    public GameObject DmgManager;
    public GameObject vidas;

    // Start is called before the first frame update
    void Start()
    {
        DmgManager = GameObject.Find("DmgManager");
        string nickname = PhotonNetwork.NickName;

        dmgPlayer = DmgManager.GetComponent<DmgManager>().dmgPlayer1;
        vidas = DmgManager.GetComponent<DmgManager>().vidasPlayer1;

        if (nickname.Equals("Player 2"))
        {
            dmgPlayer = DmgManager.GetComponent<DmgManager>().dmgPlayer2;
            vidas = DmgManager.GetComponent<DmgManager>().vidasPlayer2;
        }
        else if (nickname.Equals("Player 3"))
        {
            dmgPlayer = DmgManager.GetComponent<DmgManager>().dmgPlayer3;
            vidas = DmgManager.GetComponent<DmgManager>().vidasPlayer3;
        }
        else if (nickname.Equals("Player 4"))
        {
            dmgPlayer = DmgManager.GetComponent<DmgManager>().dmgPlayer4;
            vidas = DmgManager.GetComponent<DmgManager>().vidasPlayer4;
        }

        dmgPlayer.text = "10%";
    }

    void updateDmgPercentTxt(string dmg)
    {
        dmgPlayer.text = dmg;
    }
}
