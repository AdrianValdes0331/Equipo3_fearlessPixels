using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BangAttack : MonoBehaviour
{
    public abstract void BangStart(PlayerController player);
    public abstract void BangUpdate(PlayerController player);
}
