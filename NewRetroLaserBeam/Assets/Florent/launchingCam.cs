using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Cinemachine;

public class launchingCam : MonoBehaviour
{


    public PlayableDirector camDir;
    public CinemachinePathBase m_Path;
    public CinemachineDollyCart dollyOne;
    public GameObject sphere;

    public bool blocking = true;


    public int enemyToDie = 5;

    public int waypointNb;




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(enemyToDie);
        //Debug.Log(dollyOne.m_Position);

        if (dollyOne.m_Position >= waypointNb)
        {
            camDir.Pause();
            dollyOne.m_Speed = 0;

        }


        if (enemyToDie <= 0)
        {
            camDir.Play();
            dollyOne.m_Speed = 1;

        }

        if (dollyOne.m_Position >= 2 && enemyToDie > 0)
        {
            camDir.Pause();
            dollyOne.m_Speed = 0;
        }





        if (Input.GetMouseButtonDown(0))
        {
            enemyToDie -= 1;
        }


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
