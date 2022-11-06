using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDmg : MonoBehaviour
{

    private GameObject[] players;
    private int playerCount;
    private RectTransform canvasTransform;
    public GameObject prefab;
    public GameObject vida;
    public GameObject P1;
    public GameObject P2;
    public GameObject P3;
    public GameObject P4;
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
        players = GameObject.FindGameObjectsWithTag("Player");
        playerCount = players.Length;
        canvasTransform = GetComponent<RectTransform>();
        float d = (canvasTransform.rect.width / 1.1f) / (playerCount + 1);
        float a = canvasTransform.rect.width / -2.2f;
        float b = canvasTransform.rect.width / 2;
        Vector3 pos = transform.position;
        Vector3 Livepos = transform.position;
        Vector3 PIndexPos = transform.position;
        pos.y = (canvasTransform.rect.height / (2.0f/canvasTransform.localScale.y)) * -1;
        Livepos.y = (canvasTransform.rect.height / (2.61f/canvasTransform.localScale.y)) * -1;
        PIndexPos.y = (canvasTransform.rect.height / (2.37f / canvasTransform.localScale.y)) * -1;
        Debug.Log(d);
        Debug.Log(a);
        Debug.Log(b);

        for (int i = 0; i < playerCount; i++)
        {
            GameObject CurrPlayer = P1;
            if (i == 0)
            {
                CurrPlayer = P1;
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
            Fixedx = canvasTransform.localScale.x;
            int lives = players[0].GetComponent<Respawn>().lives;
            Debug.Log(a);
            Debug.Log(b);
            Debug.Log(a + (i + 1) * d);
            pos.x = a + (i + 1) * d;
            PIndexPos.x = a + (i + 1) * d;
            Debug.Log(pos.x);
            GameObject instance = Instantiate(prefab, pos+transform.position, Quaternion.identity, gameObject.transform);
            instance.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
            instance.name = players[i].name + "Profile" + i;
            //instance.transform.SetParent(transform, false);
            playerProfile.Add(players[i].transform.parent.name, instance);
            GameObject Index = Instantiate(CurrPlayer, PIndexPos + transform.position, Quaternion.identity, instance.transform);
            for (int j = 0; j < lives; j++)
            {
                Livepos.x = a + (i + 1) * d + Fixedx;
                GameObject live = Instantiate(vida, Livepos + transform.position, Quaternion.identity, instance.transform);
                live.name = "vida" + j;
                Fixedx = Fixedx + canvasTransform.localScale.x*25.0f;
            }         
        }
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        runScript();
    }

}
