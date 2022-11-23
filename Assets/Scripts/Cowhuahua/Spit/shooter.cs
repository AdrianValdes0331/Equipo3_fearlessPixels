using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooter : MonoBehaviour
{
    Vector2 Direction;
    public float force;
    public GameObject PointPrefab;
    public GameObject[] Points;
    public int numberOfPoints;
    // Start is called before the first frame update
    void Start()
    {
        Points = new GameObject[numberOfPoints];
        for (int i = 0; i< numberOfPoints; i++)
        {
            Points[i] = Instantiate(PointPrefab, transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 bowPos = transform.position;
        Direction = MousePos - bowPos;
        faceMouse();
        for (int i = 0; i < Points.Length; i++)
        {
            Points[i].transform.position = PointPosition(i*0.1f);
        }
    }

    void faceMouse()
    {
        transform.right = Direction;
    }

    Vector2 PointPosition(float t)
    {
        Vector2 currentPointPos = (Vector2)transform.position + (Direction.normalized * force * t) + 0.5f * Physics2D.gravity * (t*t);
        return currentPointPos;
    }
}
