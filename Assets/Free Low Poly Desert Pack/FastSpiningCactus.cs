using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastSpiningCactus : MonoBehaviour
{
    public float speed = 1;
    private float y = -5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {      
        y = Mathf.PingPong(Time.time * speed, 4) -1 - 5;
        transform.position = new Vector3(0, y, 15);
        transform.Rotate(Vector3.up * 700 * Time.deltaTime, Space.Self);
    }
}
