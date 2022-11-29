using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChinchiSpecial : Special
{
    bool done;
    Vector2 i_movement;
    float pSize;
    [SerializeField] GameObject Scope;
    [SerializeField] Misil Misil;
    [SerializeField] Transform ScopeSpawn;
    [SerializeField] Transform FirePoint;
    GameObject scope;
    Misil misil;
    public float speed;

    public override void SpecialStart(PlayerController player)
    {
        done = false;
        scope = Instantiate(Scope, ScopeSpawn.position, Quaternion.identity);
        misil = Instantiate(Misil, FirePoint.position, Quaternion.identity);
        misil.target = scope;
        misil.bang = player.gameObject.GetComponent<BangLvl>();
        misil.player = player.transform;
        misil.exit += ExitState;
    }

    public override void SpecialUpdate(PlayerController player)
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
