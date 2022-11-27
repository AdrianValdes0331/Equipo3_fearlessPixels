using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChinSpawner : MonoBehaviour
{
    public Transform pos;
    public GameObject Chins;
    public int i = 0;

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad > i)
        {
            int spawnran = Random.Range(20, 25);
            Instantiate(Chins, pos.position, Quaternion.identity);
            i += spawnran;
        }
    }
}
