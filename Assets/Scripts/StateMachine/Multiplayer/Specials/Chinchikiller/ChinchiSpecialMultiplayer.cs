using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

[System.Serializable]
public class ChinchiSpecialMultiplayer : SpecialMultiplayer
{
    bool done;
    Vector2 i_movement;
    float pSize;
    [SerializeField] GameObject Scope;
    [SerializeField] MisilMultiplayer Misil;
    [SerializeField] Transform ScopeSpawn;
    [SerializeField] Transform FirePoint;
    GameObject scope;
    MisilMultiplayer misil;
    public float speed;


    public override void SpecialStart(MultiplayerControllerSM player)
    {
        misil = PhotonNetwork.Instantiate(Misil.name, FirePoint.position, Quaternion.identity).GetComponent<MisilMultiplayer>();
        done = false;
        scope = PhotonNetwork.Instantiate(Scope.name, ScopeSpawn.position, Quaternion.identity);
        misil.exit += ExitState;
        if (photonView.IsMine)
        {
            misil.photonView.RPC("updateMisilObject", RpcTarget.All, scope.tag, player.name);
        }

    }

    public override void SpecialUpdate(MultiplayerControllerSM player)
    {
        i_movement = player.i_movement;
        if (done)
        {
            misil.exit -= ExitState;
            if (i_movement.x == 0)
            {
                player.TransitionToState(player.IdleState);
            }
            else
            {
                player.TransitionToState(player.WalkState);
            }
        }
        else if (scope != null)
        {
            scope.GetComponent<Rigidbody2D>().velocity = i_movement*speed;
        }
    }

    public void ExitState()
    {
        done = true;
    }

}
