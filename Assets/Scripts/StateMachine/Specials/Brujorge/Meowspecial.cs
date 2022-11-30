using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meowspecial : Special
{
    Vector2 i_movement;
    float pSize;
    [SerializeField] FireballSM fb;
    FireballSM fireball;

    public override void SpecialStart(PlayerController player)
    {
        if (GameObject.FindGameObjectsWithTag("Fireball").Length > 4)
        {
            if (player.i_movement.x == 0)
            {
                player.TransitionToState(player.IdleState);
            }
            else
            {
                player.TransitionToState(player.WalkState);
            }
            return;
        }
        done = false;
        pSize = System.Math.Abs(player.transform.localScale.x);
        if (player.transform.localScale.x < 0)
        {
            Quaternion finalRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + 180);
            fireball = Instantiate(fb, transform.position + Vector3.left * 1.05f, finalRotation);
        }
        else
        {
            fireball = Instantiate(fb, transform.position + Vector3.right * 1.05f, transform.rotation);
        }
        fireball.player = player.transform;
/*        misil.target = scope;
        misil.bang = player.gameObject.GetComponent<BangLvl>();
        misil.player = player.transform;
        misil.exit += ExitState;*/
    }

    public override void SpecialUpdate(PlayerController player)
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
    }
}
