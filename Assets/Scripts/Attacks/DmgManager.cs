using System.Collections;
using System.Collections.Generic;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class DmgManager : MonoBehaviourPunCallbacks
{

    public TextMeshProUGUI dmgPlayer1;
    public TextMeshProUGUI dmgPlayer2;
    public TextMeshProUGUI dmgPlayer3;
    public TextMeshProUGUI dmgPlayer4;

    public GameObject vidasPlayer1;
    public GameObject vidasPlayer2;
    public GameObject vidasPlayer3;
    public GameObject vidasPlayer4;

    
    public void updateDmgPercentTxt(string dmg, string player)
    {
        if (player.Equals("P1"))
        {
            photonView.RPC("updateplayer1Dmg", RpcTarget.All, dmg);
        } else if (player.Equals("P2"))
        {
            photonView.RPC("updateplayer2Dmg", RpcTarget.All, dmg);
        } else if (player.Equals("P3"))
        {
            photonView.RPC("updateplayer3Dmg", RpcTarget.All, dmg);
        } else if (player.Equals("P4"))
        {
            photonView.RPC("updateplayer4Dmg", RpcTarget.All, dmg);
        }
    }

    [PunRPC]
    void updateplayer1Dmg(string dmg)
    {
        dmgPlayer1.text = dmg;
    }

    [PunRPC]
    void updateplayer2Dmg(string dmg)
    {
        dmgPlayer2.text = dmg;
    }

    [PunRPC]
    void updateplayer3Dmg(string dmg)
    {
        dmgPlayer3.text = dmg;
    }

    [PunRPC]
    void updateplayer4Dmg(string dmg)
    {
        dmgPlayer4.text = dmg;
    }

}
