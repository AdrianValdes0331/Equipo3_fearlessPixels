using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangSpit : MonoBehaviour
{
    public GameObject ToxSpit;
    public Transform point;

    void OnBang()
    {
        //Misil
        BangLvl bang = transform.parent.GetComponent<BangLvl>();

        if (bang.tryBang())
        {
            Instantiate(ToxSpit, point.position, point.rotation, transform.parent);
        }
    }
}
