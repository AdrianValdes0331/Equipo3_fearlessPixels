using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusPoint : MonoBehaviour
{
    Vector3 upperLeftPoint, bottomRightPoint;
    public float halfXBounds, halfYBounds, halfZBounds;
    public Bounds focusBounds;


    void Start()
    {
        upperLeftPoint = GameObject.Find("FocusLimits/UpperLeftPoint").transform.position;
        bottomRightPoint = GameObject.Find("FocusLimits/BottomRightPoint").transform.position;
        halfXBounds = (bottomRightPoint.x + Mathf.Abs(upperLeftPoint.x)) / 2;
        halfYBounds = (upperLeftPoint.y + Mathf.Abs(bottomRightPoint.y)) / 2;
        halfZBounds = (bottomRightPoint.z + Mathf.Abs(upperLeftPoint.z)) / 2;
    }

    void Update()
    {
        Vector3 stageFocusPosition = gameObject.transform.position;
        Bounds bounds = new Bounds();
        bounds.Encapsulate(new Vector3(stageFocusPosition.x - halfXBounds, stageFocusPosition.y - halfYBounds, stageFocusPosition.z - halfZBounds));
        bounds.Encapsulate(new Vector3(stageFocusPosition.x + halfXBounds, stageFocusPosition.y + halfYBounds, stageFocusPosition.z + halfZBounds));
        focusBounds = bounds;
    }
}
