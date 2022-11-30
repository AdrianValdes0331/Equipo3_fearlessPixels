using System.Collections;
using System.Collections.Generic;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class DmgManager : MonoBehaviourPunCallbacks
{

    //Singleton
    public static DmgManager instance;

    public Image imagePlayer1;
    public Image imagePlayer2;
    public Image imagePlayer3;
    public Image imagePlayer4;

    public Sprite[] playerSprites;

    public TextMeshProUGUI dmgPlayer1;
    public TextMeshProUGUI dmgPlayer2;
    public TextMeshProUGUI dmgPlayer3;
    public TextMeshProUGUI dmgPlayer4;

    public GameObject vidasPlayer1;
    public GameObject vidasPlayer2;
    public GameObject vidasPlayer3;
    public GameObject vidasPlayer4;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            gameObject.SetActive(false);
        }
        else
        {
            instance = this;
        }
    }

    public void updateBangSprite(int sprite, string player)
    {
        if (player.Equals("P1"))
        {
            photonView.RPC("updatePlayer1BangImg", RpcTarget.All, sprite);
        }
        else if (player.Equals("P2"))
        {
            photonView.RPC("updatePlayer2BangImg", RpcTarget.All, sprite);
        }
        else if (player.Equals("P3"))
        {
            photonView.RPC("updatePlayer3BangImg", RpcTarget.All, sprite);
        }
        else if (player.Equals("P4"))
        {
            photonView.RPC("updatePlayer4BangImg", RpcTarget.All, sprite);
        }
    }

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
    void updatePlayer1BangImg(int sprite)
    {
        imagePlayer1.sprite = playerSprites[sprite];
    }

    [PunRPC]
    void updatePlayer2BangImg(int sprite)
    {
        imagePlayer2.sprite = playerSprites[sprite];
    }

    [PunRPC]
    void updatePlayer3BangImg(int sprite)
    {
        imagePlayer3.sprite = playerSprites[sprite];
    }

    [PunRPC]
    void updatePlayer4BangImg(int sprite)
    {
        imagePlayer4.sprite = playerSprites[sprite];
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
