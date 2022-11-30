using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        done = false;
        scope = Instantiate(Scope, ScopeSpawn.position, Quaternion.identity);
        misil = Instantiate(Misil, FirePoint.position, Quaternion.identity);
        misil.target = scope;
        misil.bang = player.gameObject.GetComponent<BangLvlMultiplayer>();
        misil.player = player.transform;
        misil.exit += ExitState;
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
