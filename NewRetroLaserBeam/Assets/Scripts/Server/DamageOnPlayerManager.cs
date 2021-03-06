﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DamageOnPlayerManager : MonoBehaviour {

    public static DamageOnPlayerManager instance;

    private List<EnemyBehaviour> attackingEnemy;

    private Shaker cameraShaker;
    private bool cameraShakerIsRunning = false;
    public CinemachineVirtualCamera virtualCam;
    public CinemachineVirtualCamera shakeCamera;
    CinemachineBrain cinemachineBrain;

    /*IEnumerator cameraShakerCoroutine()
   {
       cameraShakerIsRunning = true;

       virtualCam.gameObject.SetActive(false);
       shakeCamera.gameObject.SetActive(true);

       while (true)
       {

           yield return new WaitForSeconds(0.8f);
       }

   }*/

    void Start()
    {    
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else instance = this;
        attackingEnemy = new List<EnemyBehaviour>();
        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
    }

    public void addAttackingEnemy(EnemyBehaviour enemy)
    {
        /*if (cameraShakerIsRunning == false)
            StartCoroutine("cameraShakerCoroutine");*/

        attackingEnemy.Add(enemy);
    }

    public void deleteAttackingEnemy(EnemyBehaviour enemy)
    {
        if (attackingEnemy.Contains(enemy))
        {
            attackingEnemy.Remove(enemy);
            Debug.Log("Delete " + enemy.gameObject.name);
            if (attackingEnemy.Count == 0)
                StopCoroutine("cameraShakerCoroutine");
        }

    }
}

