using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITesting : StateMachineBehaviour
{

    Transform player;
    public float speed = 3f;
    Rigidbody2D enemyBody;
    public bool EnableDoubleJump = true;
    bool canDoubleJump = true;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyBody = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 target = new Vector2(player.position.x, enemyBody.position.y);
        if(player.position.x < 7.2 && player.position.x > -7.2)
        {
            Vector2 newPosition = Vector2.MoveTowards(enemyBody.position, target, speed * Time.fixedDeltaTime);
            enemyBody.MovePosition(newPosition);
        }
        
    }
}
