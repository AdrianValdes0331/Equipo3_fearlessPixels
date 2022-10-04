using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{

    [SerializeField] public GameObject player;
    [SerializeField] public Transform spawnPoint;
    [SerializeField] float spawnValue;


    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y < -8)
        {
            RespawnPoint();
        }
    }

    void RespawnPoint()
    {
        player.transform.position = new Vector2(0,0);
    }
}
