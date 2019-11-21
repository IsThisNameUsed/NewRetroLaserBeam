using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGetCloseBehaviour : StateMachineBehaviour {
    GameObject gameObject;
    MeleeEnemyBehaviour enemyBehaviour;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        gameObject = animator.gameObject;
        enemyBehaviour = gameObject.GetComponent<MeleeEnemyBehaviour>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //gameObject.GetComponent<EnemyBehaviour>().GoToLocation();
        animator.transform.position += (Camera.main.transform.position - animator.transform.position).normalized * Time.deltaTime * enemyBehaviour.moveSpeed;
        if (Vector3.Distance(gameObject.transform.position, Camera.main.transform.position) < enemyBehaviour.stopDistance)
        {
            enemyBehaviour.EnemyIsClose(true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
