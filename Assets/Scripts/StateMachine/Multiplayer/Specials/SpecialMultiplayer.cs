using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public abstract class SpecialMultiplayer : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public abstract void SpecialStart(MultiplayerControllerSM player);
    public abstract void SpecialUpdate(MultiplayerControllerSM player);
}
