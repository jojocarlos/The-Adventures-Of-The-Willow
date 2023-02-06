using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_Back : StateMachineBehaviour
{
	[SerializeField] private float speedMove;
    private Vector3 initialPoint;
    private Bat bat;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bat = animator.gameObject.GetComponent<Bat>();
	    initialPoint = bat.initialPoint;       
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		animator.transform.position = Vector2.MoveTowards(animator.transform.position, initialPoint, speedMove * Time.deltaTime);
	    bat.TurnBat(initialPoint);
	    if (animator.transform.position == initialPoint)
	    {
	        animator.SetTrigger("Later");
	    }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
