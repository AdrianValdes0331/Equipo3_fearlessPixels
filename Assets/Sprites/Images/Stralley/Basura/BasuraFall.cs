using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasuraFall : MonoBehaviour
{
    public Transform Postition1;
    public Transform Postition2;
    public Transform Postition3;
    public Transform Postition4;
    public Transform Postition5;
    public Transform basura;
    public int i = 0;
    
// Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Time.timeSinceLevelLoad > i && (GameObject.FindWithTag("Basura") == null))
        {
            int rInt = Random.Range(0, 4);
            int spawnran = Random.Range(20, 35);

            if (rInt == 0)
            {
                Instantiate(basura, Postition1.position, Quaternion.identity);
            }
            if (rInt == 1)
            {
                Instantiate(basura, Postition2.position, Quaternion.identity);
            }
            if (rInt == 2)
            {
                Instantiate(basura, Postition3.position, Quaternion.identity);
            }
            if (rInt == 3)
            {
                Instantiate(basura, Postition4.position, Quaternion.identity);
            }
            if (rInt == 4)
            {
                Instantiate(basura, Postition5.position, Quaternion.identity);
            }

            Destroy(GameObject.FindWithTag("Basura"), 5);

            i += spawnran;
        }
        
    }



 
}
