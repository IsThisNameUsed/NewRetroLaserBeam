using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootBehaviour : StateMachineBehaviour
{
    private float timer;
    private float timerBeforeShooting;
    private bool isShooting = false;
    private ShooterEnemyBehaviour shooterEnemyBehaviour;

    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        shooterEnemyBehaviour = animator.gameObject.GetComponent<ShooterEnemyBehaviour>();
        if (shooterEnemyBehaviour == null)
            Debug.Log("enemyBehaviour NULL");
        timerBeforeShooting = shooterEnemyBehaviour.timerBeforeShooting;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (!isShooting)
        {
            WaitBeforeFirstShoot();
            return;
        }

        timer += Time.deltaTime;
        if(timer >= shooterEnemyBehaviour.hitTime * 5)
        {
            timer = 0;
            shooterEnemyBehaviour.targetedPlayer.TakeDamage(ref shooterEnemyBehaviour.damagePoint);
        }
        
    }

    private void WaitBeforeFirstShoot()
    {
        timer += Time.deltaTime;
        if (timer >= timerBeforeShooting)
        {
            isShooting = true;
            timer = 0;
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

