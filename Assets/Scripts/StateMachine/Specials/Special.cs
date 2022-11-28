using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Special : MonoBehaviour
{
    // Start is called before the first frame update
    public abstract void SpecialStart(PlayerController player);
    public abstract void SpecialUpdate(PlayerController player);
}
