using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    public GameObject ToxSpit;
    public Transform point;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Spit();
        }
    }

    void Spit()
    {
        Instantiate(ToxSpit, point.position, point.rotation, transform.parent.transform.parent);
    }
}
