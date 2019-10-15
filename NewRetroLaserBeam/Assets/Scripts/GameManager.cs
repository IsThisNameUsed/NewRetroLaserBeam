using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public  class GameManager : MonoBehaviour {

    public static GameManager instance;
    private List<EnemyBehaviour> attackingEnemy;


    private Shaker cameraShaker;
    private bool cameraShakerIsRunning = false;
    public CinemachineVirtualCamera virtualCam;
    public CinemachineVirtualCamera shakeCamera;
    CinemachineBrain cinemachineBrain;
    // Use this for initialization

    IEnumerator cameraShakerCoroutine()
    {
        cameraShakerIsRunning = true;
       
        virtualCam.gameObject.SetActive(false);
        shakeCamera.gameObject.SetActive(true);
        

        while (true)
        {
            
            yield return new WaitForSeconds(0.8f);
        }
        
    }

    void Start () {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else instance = this;
        
        attackingEnemy = new List<EnemyBehaviour>();
        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void addAttackingEnemy(EnemyBehaviour enemy)
    {
        if (cameraShakerIsRunning == false)
            StartCoroutine("cameraShakerCoroutine");

        attackingEnemy.Add(enemy);
        Debug.Log("add " + enemy.gameObject.name);
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
