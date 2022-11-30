using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cowhuabang : BangAttack
{
    public Cowhuasplode ToxSpit;
    private Cowhuasplode spit;
    public Transform point;
    Vector2 i_movement;
    float pSize;

    public override void BangStart(PlayerController player)
    {
        done = false;
        pSize = System.Math.Abs(player.transform.localScale.x);
        spit = Instantiate(ToxSpit, point.position, point.rotation);
        spit.scale = player.transform.localScale;
        spit.player = player.transform;
        spit.exit += ExitState;
    }

    public override void BangUpdate(PlayerController player)
    {
        i_movement = player.i_movement;
        player.rb.velocity = new Vector2(i_movement.x * player.speed, player.rb.velocity.y);

        if (i_movement.x < 0.0f)
        {
            player.transform.localScale = new Vector3(-pSize, pSize, pSize);
        }
        else if (i_movement.x > 0.0f)
        {
            player.transform.localScale = new Vector3(pSize, pSize, pSize);
        }

        if (done)
        {
            spit.exit -= ExitState;
            if (player.rb.velocity.x == 0)
            {
                player.TransitionToState(player.IdleState);
            }
            else
            {
                player.TransitionToState(player.WalkState);
            }
        }
    }
}
