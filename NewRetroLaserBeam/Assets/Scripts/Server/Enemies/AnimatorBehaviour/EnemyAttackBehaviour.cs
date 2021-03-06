﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackBehaviour : StateMachineBehaviour {

    float timer = 0;
    MeleeEnemyBehaviour enemyBehaviour;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyBehaviour = animator.gameObject.GetComponent<MeleeEnemyBehaviour>();
        if (enemyBehaviour == null)
            Debug.Log("enemyBehaviour NULL");


        DamageOnPlayerManager.instance.addAttackingEnemy(enemyBehaviour);


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //damage here
        timer += Time.deltaTime;
        if(timer >= GameManager.instance.timeToCheckHitOnEnnemy *5)
        {
            timer = 0;
            enemyBehaviour.targetedPlayer.TakeDamage(ref enemyBehaviour.damagePoint);
            enemyBehaviour.Attack();
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
