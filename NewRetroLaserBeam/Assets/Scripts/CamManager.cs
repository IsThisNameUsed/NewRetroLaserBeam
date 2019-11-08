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

    public int waypointNb = 3;
    public int currentWayPoint = 1;
    public int[] numberOfenemiesPerWayPoint;
    public float[] times;
    public GameObject camChild;

    public PlayableDirector playableDirector;

    private bool GameIsActiv = false; //If we need to pause the the game use this

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else instance = this;
    }


    void Start()
    {    
        enemyToDie = numberOfenemiesPerWayPoint[currentWayPoint];
        //playableDirector = GetComponent<PlayableDirector>();
        times = new float[5];
        times[1] = 1.40f;
        times[2] = 2.25f;
        times[3] = 10f;
        times[4] = 18f;
        Debug.Log(playableDirector.time);
        Debug.Log(times[4]);
        playableDirector.Pause();
    }

    // Update is called once per frame
 
    void Update()
    { 
        if (!GameIsActiv)
            return;
        else playableDirector.Play();

        if (enemyToDie <= 0)
        {
            if (currentWayPoint < waypointNb)
                currentWayPoint += 1;
            else SceneManager.LoadScene("KOM_SERVER 1");
            if (playableDirector.time < times[currentWayPoint])
            {
                playableDirector.Play();
                enemyToDie = numberOfenemiesPerWayPoint[currentWayPoint];
                dollyOne.m_Speed = 1;
            }
        }

        if (playableDirector.time >= times[currentWayPoint] && enemyToDie > 0)
        {
            playableDirector.Pause();
            dollyOne.m_Speed = 0;
        }

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
