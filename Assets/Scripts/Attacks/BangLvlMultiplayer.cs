using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class BangLvlMultiplayer : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    private int bangLvl;
    private float totalDmgDiff;
    private bool isAvailable;
    private IEnumerator coroutine;
    public int cd;
    public int normal;
    public int Bang1;
    public int Bang2;
    public int Bang3;
    public string actualPlayer;
    

    void Start()
    {
        bangLvl = 0;
        totalDmgDiff = 0;
        isAvailable = true;
        coroutine = WaitAndPrint(0.3f);
        StartCoroutine(coroutine);

        actualPlayer = "P1";
        if(PhotonNetwork.NickName.Equals("Player 2"))
        {
            actualPlayer = "P2";
        } else if (PhotonNetwork.NickName.Equals("Player 3"))
        {
            actualPlayer = "P3";
        } else if(PhotonNetwork.NickName.Equals("Player 4"))
        {
            actualPlayer = "P4";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void updateBangImage(int img)
    {

        //Debug.Log(GameObject.Find("Canvas").GetComponent<PlayerDmg>().playerProfile[gameObject.name]);
        DmgManager.instance.updateBangSprite(img, actualPlayer);
        //nImg = img;

    }

    public void bangUpdate(float dmg, bool done)
    {
        float currDmg = totalDmgDiff;
        if (!isAvailable) { return; }

        if (done)
        {
            totalDmgDiff += dmg;
        }
        else
        {
            totalDmgDiff = ((totalDmgDiff - dmg/2) < 0) ? 0 : totalDmgDiff - dmg/2;
        }

        Debug.Log(totalDmgDiff);

        if (totalDmgDiff >= 70 && totalDmgDiff<115) {

            bangLvl = 1;
            updateBangImage(Bang1);
        
        }
        else if(totalDmgDiff >= 115 && totalDmgDiff < 200)
        {
            bangLvl = 2;
            updateBangImage(Bang2);
        }
        else if(totalDmgDiff >= 200)
        {
            bangLvl = 3;
            updateBangImage(Bang3);
        }
        else
        {
            bangLvl = 0;
            updateBangImage(normal);
        }

    }

    public bool tryBang()
    {

        if(bangLvl != 0 && isAvailable)
        {
            StartCoroutine(cooldown());
            updateBangImage(normal);
            return true;
        }
        return false;

    }

    public float bangModifier(float baseDmg) {

        Debug.Log(bangLvl);
        Debug.Log(baseDmg);
        Debug.Log(baseDmg+((bangLvl-1)*(baseDmg*0.25f)));
        return baseDmg+((bangLvl-1)*(baseDmg*0.25f));

    }

    public IEnumerator cooldown()
    {
        isAvailable = false;
        yield return new WaitForSeconds(cd);
        bangLvl = 0;
        totalDmgDiff = 0;
        isAvailable = true;
        updateBangImage(normal);
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        updateBangImage(normal);
    }

}
