using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class BangAttackMultiplayer : MonoBehaviourPunCallbacks
{
    public abstract void BangStart(MultiplayerControllerSM player);
    public abstract void BangUpdate(MultiplayerControllerSM player);
}
