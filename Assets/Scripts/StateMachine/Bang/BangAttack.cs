using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BangAttack : MonoBehaviour
{
    protected bool done = false;
    public abstract void BangStart(PlayerController player);
    public abstract void BangUpdate(PlayerController player);
    public void ExitState()
    {
        done = true;
    }
}
