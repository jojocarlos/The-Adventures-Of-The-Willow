using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_Follow : StateMachineBehaviour
{
	
	[SerializeField] private float speedMove;
    [SerializeField] private float timeBase;
    private float timeFollow;
    private Transform player;
    private Bat bat;
    
	//OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeFollow = timeBase;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	    bat = animator.gameObject.GetComponent<Bat>();    
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, player.position, speedMove * Time.deltaTime);
	    bat.TurnBat(player.position);
	    timeFollow -= Time.deltaTime;
	    if(timeFollow <= 0)
        {
	        animator.SetTrigger("Back");  
	    }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
            
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
