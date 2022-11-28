using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudPath : MonoBehaviour
{
    public Vector3[] currentPoint = new Vector3[6];
    List<Rigidbody2D> players = new List<Rigidbody2D>();
    int targetIndex = 0;
    Vector3 target;
    public float speed;
    void Update()
    {
        target = Vector3.MoveTowards(transform.position, currentPoint[targetIndex], Time.deltaTime * speed);
        transform.position = target;
/*        if (players.Count > 0)
        {
            for (int i = 0; i < players.Count; i++)
            {
                Debug.Log(target);
                players[i].velocity = target;
            }
        }*/
        if (transform.position == currentPoint[targetIndex])
            targetIndex = (targetIndex + 1) % currentPoint.Length;
    }

    /*private void OnEnable()
    {
        DynamicPlatforms.playerOn += AddPlayer;
        DynamicPlatforms.playerOff += RemovePlayer;
    }

    private void OnDisable()
    {
        DynamicPlatforms.playerOn -= AddPlayer;
        DynamicPlatforms.playerOff -= RemovePlayer;
    }

    void AddPlayer(Rigidbody2D rb) 
    {
        Debug.Log(rb);
        players.Add(rb);
    }
    void RemovePlayer(Rigidbody2D rb)
    {
        Debug.Log(rb);
        players.Remove(rb);
    }*/
}
