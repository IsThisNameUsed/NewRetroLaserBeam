using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;

public class camManaging : MonoBehaviour
{

    public PlayableDirector camDir;
    public CinemachineVirtualCamera vCam;
    public Transform target;

    float point;

    public float[] X;
    int i = 0;

    // Start is called before the first frame update
    void Start()
    {

        //target = vCam.GetCinemachineComponent<CinemachineTrackedDolly>().FollowTarget;

        
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(vCam.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition);
        //Debug.Log(point);
        //Debug.Log(i);
        //Debug.Log(X[i]);
        //Debug.Log(target.GetComponent<CinemachineDollyCart>().m_Position);
        
       if (vCam.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition >= i)
       //if(target.GetComponent<CinemachineDollyCart>().m_Position >= X[i])
       
        {
            camDir.Pause();
            StartCoroutine("WaitBeforeRelaunch");
        }
    }

    public IEnumerator WaitBeforeRelaunch()
    {
        
        yield return new WaitForSeconds(10f);
        i++;
        camDir.Play();
    }
}
