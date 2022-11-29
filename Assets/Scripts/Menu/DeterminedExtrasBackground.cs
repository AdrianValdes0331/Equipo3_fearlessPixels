using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeterminedExtrasBackground : MonoBehaviour
{
    public Texture cowawa;
    public Texture chinchikiller;
    public Texture CL4R174;
    public Texture foxHunter;
    public Texture brujorge;
    public RawImage fondoEscena;

    // Start is called before the first frame update
    void Awake()
    {
        if (LoadExtras.esceneType == 1)
        {
            fondoEscena.texture = chinchikiller;
        }
        else if (LoadExtras.esceneType == 2)
        {
            fondoEscena.texture = CL4R174;
        }
        else if (LoadExtras.esceneType == 3)
        {
            fondoEscena.texture = cowawa;
        }
        else if (LoadExtras.esceneType == 4)
        {
            fondoEscena.texture = foxHunter;
        }
    }
}
