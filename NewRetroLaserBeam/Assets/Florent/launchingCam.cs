using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Cinemachine;
using UnityEngine.SceneManagement;

public class launchingCam : MonoBehaviour
{

    PlayableDirector camDir;
    public CinemachinePathBase m_Path;
    public CinemachineDollyCart dollyOne;
   
    public int enemyToDie;

    public int waypointNb = 3;
    public int currentWayPoint = 1;
    public int[] numberOfenemiesPerWayPoint;
    public float[] times;
    public GameObject camChild;

    public PlayableDirector playableDirector;


    // Start is called before the first frame update
    void Start()
    {
        camDir = GetComponent<PlayableDirector>();
        enemyToDie = numberOfenemiesPerWayPoint[currentWayPoint];
        //playableDirector = GetComponent<PlayableDirector>();
        times = new float[5];
        times[1] = 1.40f;
        times[2] = 2.25f;
        times[3] = 10f;
        times[4] = 18f;
        Debug.Log(playableDirector.time);
        Debug.Log(times[4]);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(playableDirector.time);
        //Debug.Log(enemyToDie);
        //Debug.Log(dollyOne.m_Position);

        if (enemyToDie <= 0)
        {
            if (currentWayPoint < waypointNb)
                currentWayPoint += 1;
            else SceneManager.LoadScene("KOM_SERVER");
            if (playableDirector.time < times[currentWayPoint])
            {
                camDir.Play();
                enemyToDie = numberOfenemiesPerWayPoint[currentWayPoint];
                dollyOne.m_Speed = 1;
            }
        }

        if (playableDirector.time >= times[currentWayPoint] && enemyToDie > 0)
        {
            camDir.Pause();
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

    /*private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "cameraStop")
        {
            camDir.Pause();
            Debug.Log("test cam");
        }
    }*/


}
