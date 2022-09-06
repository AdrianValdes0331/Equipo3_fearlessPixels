using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDmg : MonoBehaviour
{

    private GameObject[] players;
    private int playerCount;
    private RectTransform canvasTransform;
    public GameObject prefab;
    [HideInInspector]
    public Dictionary<string, GameObject> playerProfile = new Dictionary<string, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
        players = GameObject.FindGameObjectsWithTag("Player");
        playerCount = players.Length;
        canvasTransform = GetComponent<RectTransform>();
        float d = canvasTransform.rect.width/(playerCount+1);
        float a = canvasTransform.rect.width/-2;
        float b = canvasTransform.rect.width/2;
        Vector3 pos = transform.position;
        pos.y=-127;
        Debug.Log(d);
        Debug.Log(a);
        Debug.Log(b);
        
        for(int i = 0; i<playerCount; i++){

            Debug.Log(a+(i+1)*d);
            pos.x = a+(i+1)*d;
            Debug.Log(pos.x);
            GameObject instance = Instantiate(prefab, pos*canvasTransform.localScale.x, Quaternion.identity, gameObject.transform);
            instance.name=players[i].name+"Dmg";
            playerProfile.Add(players[i].name, instance);

        }

    }

}
