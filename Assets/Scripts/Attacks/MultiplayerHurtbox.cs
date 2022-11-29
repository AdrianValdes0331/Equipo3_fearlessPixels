using System.Collections;
using System.Collections.Generic;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class MultiplayerHurtbox : MonoBehaviourPunCallbacks
{
    private Collider2D hcollider;
    public float tankiness = 2.0f;
    [HideInInspector] public float dmgPercent = 0.0f;

    public bool getHitBy(float damage, int force, int angle, float xPos)
    {

        BangLvl bang = transform.parent.transform.parent.GetComponent<BangLvl>();
        //bang.bangUpdate(damage, false);
        //alreveza el angulo dependiendo si el ataque esta a la derecha o izquierda
        if (transform.position.x - xPos < 0) { angle = 180 - angle; }
        float radian = angle * Mathf.Deg2Rad;
        Vector2 finalForce = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)) * force;
        print("Collider: " + gameObject.name);
        print("Fuerza base = " + force);
        print("angulo = " + angle);
        print("Fuerza final = " + finalForce);
        dmgPercent += damage;
        transform.parent.GetComponent<Rigidbody2D>().AddForce(finalForce * ((dmgPercent / 100) / tankiness));
        Debug.Log(transform.parent);
        photonView.RPC("UpdateDmgPercentText", RpcTarget.All);
        //UpdateDmgPercentText();
        return true;

    }

    [PunRPC]
    public void UpdateDmgPercentText()
    {
        Debug.Log("photonView.IsMine: " + photonView.IsMine);
        if (photonView.IsMine) return;
        if(PhotonNetwork.NickName.Equals("Player 1"))
        {
            DmgManager.instance.updateDmgPercentTxt(System.Math.Round(dmgPercent, 2) + "%", "P2");
        }
        else if (PhotonNetwork.NickName.Equals("Player 2"))
        {
            DmgManager.instance.updateDmgPercentTxt(System.Math.Round(dmgPercent, 2) + "%", "P1");
        }

    }

}
