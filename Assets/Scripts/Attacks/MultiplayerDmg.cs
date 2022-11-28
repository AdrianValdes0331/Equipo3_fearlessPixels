using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class MultiplayerDmg : MonoBehaviourPunCallbacks
{
    private int playerCount;
    private RectTransform canvasTransform;

    public GameObject[] playersDMG;

    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Player4;

    private IEnumerator coroutine;
    [HideInInspector]
    public Dictionary<string, GameObject> playerProfile = new Dictionary<string, GameObject>();
    private float Fixedx;

    // Start is called before the first frame update
    void Start()
    {

        coroutine = WaitAndPrint(0.2f);
        StartCoroutine(coroutine);
    }

    void runScript()
    {
        int k = 0;
        playerCount = PhotonNetwork.PlayerList.Length;
        canvasTransform = GetComponent<RectTransform>();
        float d = (canvasTransform.rect.width / 1.1f) / (playerCount + 1);
        float a = canvasTransform.rect.width / -2.2f;
        float b = canvasTransform.rect.width / 2;
        Vector3 pos = transform.position;
        Vector3 Livepos = transform.position;
        Vector3 PIndexPos = transform.position;
        pos.y = (canvasTransform.rect.height / (2.0f / canvasTransform.localScale.y)) * -1;
        Livepos.y = (canvasTransform.rect.height / (2.61f / canvasTransform.localScale.y)) * -1;
        PIndexPos.y = (canvasTransform.rect.height / (2.37f / canvasTransform.localScale.y)) * -1;
        Debug.Log(d);
        Debug.Log(a);
        Debug.Log(b);

        for (int i = 0; i < playerCount; i++)
        {
            pos.x = a + (i + 1) * d;
            PIndexPos.x = a + (i + 1) * d;


            if (i == 0)
            {
                Player1.transform.position = pos + transform.position;
                playerProfile.Add("Player1", Player1);
                Player1.transform.Find("PlayerIndex_1").transform.position = PIndexPos + transform.position;
                Livepos.x = a + (i + 1) * d + Fixedx;

                Player1.transform.Find("vidasP1").Find("vida1").transform.position = Livepos + transform.position;
                Fixedx = Fixedx + canvasTransform.localScale.x * 25.0f;

                Livepos.x = a + (i + 1) * d + Fixedx;
                Player1.transform.Find("vidasP1").Find("vida2").transform.position = Livepos + transform.position;
                Fixedx = Fixedx + canvasTransform.localScale.x * 25.0f;

                Livepos.x = a + (i + 1) * d + Fixedx;
                Player1.transform.Find("vidasP1").Find("vida3").transform.position = Livepos + transform.position;
                Fixedx = Fixedx + canvasTransform.localScale.x * 25.0f;
            }

            if (i == 1)
            {
                Player2.transform.position = pos + transform.position;
                playerProfile.Add("Player2", Player2);
                Player2.transform.Find("PlayerIndex_2").transform.position = PIndexPos + transform.position;
                Livepos.x = a + (i + 1) * d + Fixedx;

                Player2.transform.Find("vidasP2").Find("vida1").transform.position = Livepos + transform.position;
                Fixedx = Fixedx + canvasTransform.localScale.x * 25.0f;

                Livepos.x = a + (i + 1) * d + Fixedx;
                Player2.transform.Find("vidasP2").Find("vida2").transform.position = Livepos + transform.position;
                Fixedx = Fixedx + canvasTransform.localScale.x * 25.0f;

                Livepos.x = a + (i + 1) * d + Fixedx;
                Player2.transform.Find("vidasP2").Find("vida3").transform.position = Livepos + transform.position;
                Fixedx = Fixedx + canvasTransform.localScale.x * 25.0f;
            }

            if (i == 2)
            {
                Player3.transform.position = pos + transform.position;
                playerProfile.Add("Player3", Player3);
                Player3.transform.Find("PlayerIndex_3").transform.position = PIndexPos + transform.position;
                Livepos.x = a + (i + 1) * d + Fixedx;

                Player3.transform.Find("vidasP3").Find("vida1").transform.position = Livepos + transform.position;
                Fixedx = Fixedx + canvasTransform.localScale.x * 25.0f;

                Livepos.x = a + (i + 1) * d + Fixedx;
                Player3.transform.Find("vidasP3").Find("vida2").transform.position = Livepos + transform.position;
                Fixedx = Fixedx + canvasTransform.localScale.x * 25.0f;

                Livepos.x = a + (i + 1) * d + Fixedx;
                Player3.transform.Find("vidasP3").Find("vida3").transform.position = Livepos + transform.position;
                Fixedx = Fixedx + canvasTransform.localScale.x * 25.0f;
            }

            if (i == 3)
            {
                Player4.transform.position = pos + transform.position;
                playerProfile.Add("Player4", Player4);
                Player4.transform.Find("PlayerIndex_4").transform.position = PIndexPos + transform.position;
                Livepos.x = a + (i + 1) * d + Fixedx;

                Player4.transform.Find("vidasP4").Find("vida1").transform.position = Livepos + transform.position;
                Fixedx = Fixedx + canvasTransform.localScale.x * 25.0f;

                Livepos.x = a + (i + 1) * d + Fixedx;
                Player4.transform.Find("vidasP4").Find("vida2").transform.position = Livepos + transform.position;
                Fixedx = Fixedx + canvasTransform.localScale.x * 25.0f;

                Livepos.x = a + (i + 1) * d + Fixedx;
                Player4.transform.Find("vidasP4").Find("vida3").transform.position = Livepos + transform.position;
                Fixedx = Fixedx + canvasTransform.localScale.x * 25.0f;
            }
        }

        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            playersDMG[i].SetActive(true);
        }
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        runScript();
    }

}
