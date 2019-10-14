using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Cinemachine;

public class launchingCam : MonoBehaviour
{


    PlayableDirector camDir;
    public CinemachinePathBase m_Path;
    public CinemachineDollyCart dollyOne;
    


    public int enemyToDie = 5;

    public int waypointNb = 1;

    public int currentWayPoint;




    // Start is called before the first frame update
    void Start()
    {
        camDir = GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(enemyToDie);
        //Debug.Log(dollyOne.m_Position);

        if (dollyOne.m_Position >= waypointNb && enemyToDie > 0)
        {

            camDir.Pause();
            dollyOne.m_Speed = 0;

        }


        if (enemyToDie <= 0)
        {
            camDir.Play();
            dollyOne.m_Speed = 1;

        }





        if (Input.GetKey(KeyCode.A))
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
