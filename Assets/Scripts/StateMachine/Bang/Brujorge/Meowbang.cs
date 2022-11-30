using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meowbang : BangAttack
{
    public Miauterito miauPrefab;
    private List<Miauterito> miau;
    public Transform point;
    Vector2 i_movement;
    float pSize;

    public override void BangStart(PlayerController player)
    {
        miau = new List<Miauterito>();
        done = false;
        pSize = System.Math.Abs(player.transform.localScale.x);
        this.gameObject.tag = "Untagged";
        GameObject[] players = GameObject.FindGameObjectsWithTag("Driver");
        this.gameObject.tag = "Driver";
        Debug.Log(players.Length);
        for (int i = 0; i < 3; i++)
        {
            miau.Add(Instantiate(miauPrefab, point.position, point.rotation));
            if(i < players.Length)
            {
                Debug.Log(players[i]);
                miau[i].target = players[i];
            }
            else
            {
                Debug.Log(players[0]);
                miau[i].target = players[0];
            }
            miau[i].bang = player.gameObject.GetComponent<BangLvl>();
            miau[i].player = player.transform;
        }

        
        /*spit.scale = player.transform.localScale;
        spit.player = player.transform;
        spit.exit += ExitState;*/
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
