using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagFinder : MonoBehaviour
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
        coroutine = WaitAndPrint(0.5f);
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void runScript()
    {
        int k = 0;
        players = GameObject.FindGameObjectsWithTag("Player");
        playerCount = players.Length;
        GameObject CurrPlayer;
        for (int i = 0; i < playerCount; i++)
        {
            if (k == 0)
            {
                CurrPlayer = P1;
                if (GameObject.Find("Player1").transform.childCount > 0)
                {
                    Transform p1name = GameObject.Find("Player1").transform.GetChild(0).Find("Tag");
                    Debug.Log(p1name);
                    GameObject instance = Instantiate(CurrPlayer, p1name.transform.position, transform.rotation);
                    instance.transform.parent = p1name.transform;
                    instance.name = "P1Tag";
                }
                else
                {                   
                    k = 1;
                }
            }
            if (k == 1)
            {
                CurrPlayer = P2;               
                if (GameObject.Find("Player2").transform.childCount > 0)
                {
                    Transform p2name = GameObject.Find("Player2").transform.GetChild(0).Find("Tag");
                    Debug.Log(p2name);
                    GameObject instance = Instantiate(CurrPlayer, p2name.transform.position, transform.rotation);
                    instance.transform.parent = p2name.transform;
                    instance.name = "P2Tag";
                }
                else
                {                  
                    k = 2;
                }
            }
            if (k == 2)
            {
                CurrPlayer = P3;
                if (GameObject.Find("Player3").transform.childCount > 0)
                {
                    Transform p3name = GameObject.Find("Player3").transform.GetChild(0).Find("Tag");
                    Debug.Log(p3name);
                    GameObject instance = Instantiate(CurrPlayer, p3name.transform.position, transform.rotation);
                    instance.transform.parent = p3name.transform;
                    instance.name = "P3Tag";
                }
                else
                {
                    k = 3;
                }
            }
            if (k == 3)
            {
                CurrPlayer = P4;
                if (GameObject.Find("Player4").transform.childCount > 0)
                {
                    Transform p4name = GameObject.Find("Player4").transform.GetChild(0).Find("Tag");
                    Debug.Log(p4name);
                    GameObject instance = Instantiate(CurrPlayer, p4name.transform.position, transform.rotation);
                    instance.transform.parent = p4name.transform;
                    instance.name = "P4Tag";
                }
            }
            k++;
        }
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        runScript();
    }
}
