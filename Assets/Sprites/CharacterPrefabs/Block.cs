using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        float force = 5000;
        int luck = Random.Range(0, 2);
        if (collision.gameObject.layer == 7 && luck == 1)
        {
            Debug.Log("Bounce");
            Vector2 dir = collision.transform.position;
            dir = -dir.normalized;
            collision.transform.GetComponent<Rigidbody2D>().AddForce(dir * force);            
        }
    }
}
