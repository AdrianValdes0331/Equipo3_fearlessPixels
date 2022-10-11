using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{

    [SerializeField] public GameObject player;
    [SerializeField] public Transform spawnPoint;
    [SerializeField] float spawnValue;
    public int lives = 2;


    // Update is called once per frame
    void Update()
    {
        if (((player.transform.position.y < -8 || player.transform.position.y > 20) || (player.transform.position.x < -20 || player.transform.position.x > 20)) && lives > 0)
        {
            RespawnPoint();
            lives--;
            Debug.Log(lives);
        }
        if (lives <= 0)
        {
            Debug.Log("asdf");
            Destroy(transform.parent.gameObject);
        }
    }

    void RespawnPoint()
    {
        player.transform.position = new Vector2(-10,10);
    }
}
