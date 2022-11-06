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
        players = GameObject.FindGameObjectsWithTag("Player");
        playerCount = players.Length;
        for (int i = 0; i < playerCount; i++)
        {
            GameObject CurrPlayer = P1;
            if (i == 0)
            {
                CurrPlayer = P1;
                Transform p1name =  GameObject.Find("Player1").transform.Find(name);
                GameObject instance = Instantiate(CurrPlayer, GameObject.Find("Player1").transform.Find(p1name.ToString()).Find("Tag").position + 
                    transform.position, Quaternion.identity, gameObject.transform);
                //Instantiate(CurrPlayer, GameObject.Find("Player1").transform.Find(p1name.ToString()).Find("Tag").position);//, worldPositionStays: false);
            }
            if (i == 1)
            {
                CurrPlayer = P2;
            }
            if (i == 2)
            {
                CurrPlayer = P3;
            }
            if (i == 3)
            {
                CurrPlayer = P4;
            }
            //Instantiate(CurrPlayer, , worldPositionStays: false);
        }
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        runScript();
    }
}
