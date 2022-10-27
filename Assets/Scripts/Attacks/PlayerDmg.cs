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
    private IEnumerator coroutine;
    [HideInInspector]
    public Dictionary<string, GameObject> playerProfile = new Dictionary<string, GameObject>();
    private int Fixedx;

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
        float d = canvasTransform.rect.width / (playerCount + 1);
        float a = canvasTransform.rect.width / -2;
        float b = canvasTransform.rect.width / 2;
        Vector3 pos = transform.position;
        Vector3 Livepos = transform.position;
        pos.y = -475;
        Livepos.y = -190;
        Debug.Log(d);
        Debug.Log(a);
        Debug.Log(b);

        for (int i = 0; i < playerCount; i++)
        {
            Fixedx = -10;
            int lives = players[0].GetComponent<Respawn>().lives;
            Debug.Log(a);
            Debug.Log(b);
            Debug.Log(a + (i + 1) * d);
            pos.x = a + (i + 1) * d;
            Debug.Log(pos.x);
            GameObject instance = Instantiate(prefab, pos+transform.position, Quaternion.identity, gameObject.transform);
            instance.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            instance.name = players[i].name + "Profile";
            playerProfile.Add(players[i].name, instance);
            for (int j = 0; j < lives; j++)
            {
                Livepos.x = a + (i + 1) * d + Fixedx;
                GameObject live = Instantiate(vida, Livepos + transform.position, Quaternion.identity, instance.transform);
                live.name = "vida" + j;
                Fixedx = Fixedx + 10;
            }         
        }
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        runScript();
    }

}
