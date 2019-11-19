using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Cinemachine;
using UnityEngine.SceneManagement;

public class CamManager : MonoBehaviour
{
    public static CamManager instance;

    public CinemachinePathBase m_Path;
    public CinemachineDollyCart dollyOne;
   
    public int enemyToDie;
    private bool stopCamera;
    public int waypointNb = 3;
    public int currentWayPoint = 1;
    public int[] numberOfenemiesPerWayPoint;
    public float[] times;
    public GameObject camChild;

    public PlayableDirector playableDirector;

    public bool GameIsActiv = false; //If we need to pause the the game use this

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else instance = this;
        //Attention!! Doit être appelé avant le awake du game manager -> régler la priorité d'exécution des scripts
        playableDirector.Pause();
    }


    void Start()
    {    
        enemyToDie = numberOfenemiesPerWayPoint[currentWayPoint-1];
        //playableDirector = GetComponent<PlayableDirector>();
        times = new float[5];
        times[1] = 1.40f;
        times[2] = 2.25f;
        times[3] = 10f;
        times[4] = 18f;
        //Debug.Log(playableDirector.time);     
    }

    // Update is called once per frame
 
    void Update()
    {
        Debug.Log("dolly" + dollyOne.m_Position);
        Debug.Log("way point " + currentWayPoint);
        Debug.Log(dollyOne.m_Position >= currentWayPoint);
        Debug.Log("to die " + enemyToDie);
        Debug.Log("number " + numberOfenemiesPerWayPoint[currentWayPoint - 1]);
        if (!GameIsActiv)
            return;

        if (!stopCamera)
        {
            if(dollyOne.m_Position >= currentWayPoint)
            {
                dollyOne.m_Speed = 0;
                playableDirector.Pause();
                stopCamera = true;
            }
        }
        else
        {
            if(enemyToDie <= 0)
            {
                
                if (currentWayPoint < waypointNb)
                {
                    currentWayPoint += 1;
                    enemyToDie = numberOfenemiesPerWayPoint[currentWayPoint - 1];
                    stopCamera = false;
                    dollyOne.m_Speed = 0.5f;
                    playableDirector.Play();
                }
            }
        }

        /*if (enemyToDie <= 0)
        {
            if (currentWayPoint < waypointNb)
                currentWayPoint += 1;
            //else SceneManager.LoadScene("KOM_SERVER 1");
            if (playableDirector.time < times[currentWayPoint])
            {
                playableDirector.Play();
                enemyToDie = numberOfenemiesPerWayPoint[currentWayPoint];
                dollyOne.m_Speed = 1;
            }
        }*/

        /*if (playableDirector.time >= times[currentWayPoint] && enemyToDie > 0)
        {
            playableDirector.Pause();
            dollyOne.m_Speed = 0;
        }*/

        if (Input.GetKeyDown(KeyCode.A))
        {
            enemyToDie -= 1;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("KOM_SERVER");
        }
    }

    public void DestroyEnemy()
    {
        enemyToDie -= 1;
    }

    public void SetGameActiv(bool isActiv)
    {
        GameIsActiv = isActiv;
        if (isActiv)
        {
            Debug.Log("Play");
            playableDirector.Play();
        }
            
        else playableDirector.Pause();
    }
    /*private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "cameraStop")
        {
            camDir.Pause();
            Debug.Log("test cam");
        }
    }*/
}
