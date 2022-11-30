using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class ChinchibangMultiplayer : BangAttackMultiplayer
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

    public override void BangStart(MultiplayerControllerSM player)
    {
        done = false;
        misil = PhotonNetwork.Instantiate(Misil.name, FirePoint.position, Quaternion.identity).GetComponent<MisilMultiplayer>();
        scope = PhotonNetwork.Instantiate(Scope.name, ScopeSpawn.position, Quaternion.identity);
        misil.exit += ExitState;
        if (photonView.IsMine)
        {
            misil.photonView.RPC("updateMisilObject", RpcTarget.All, scope.tag, player.name);
        }
    }

    public override void BangUpdate(MultiplayerControllerSM player)
    {
        i_movement = player.i_movement;
        if(player.rb.velocity.x != 0)
        {
            player.rb.velocity = new Vector2(0, player.rb.velocity.y);
        }
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
