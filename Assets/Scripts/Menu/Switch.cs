using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{
    public RawImage On;
    public RawImage Off;
    public RawImage ImgOn;
    public RawImage ImgOff;
    int index;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(index == 1)
        {
            ImgOn.gameObject.SetActive(false);
            ImgOff.gameObject.SetActive(true);
        }

        if(index == 0)
        {
            ImgOn.gameObject.SetActive(true);
            ImgOff.gameObject.SetActive(false);
        }
    }

    public void ON()
    {
        index = 1;
        Off.gameObject.SetActive(true);
        On.gameObject.SetActive(false);
    }

    public void OFF()
    {
        index = 0;
        On.gameObject.SetActive(true);
        Off.gameObject.SetActive(false);
    }
}
