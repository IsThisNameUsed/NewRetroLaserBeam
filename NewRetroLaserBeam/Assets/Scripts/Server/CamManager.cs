using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Cinemachine;
using UnityEngine.SceneManagement;

public class CamManager : MonoBehaviour
{
    public List<List<EnemyBehaviour>> wavesList;
    public List<int> numberOfenemiesPerWayPoint;

    public static CamManager instance;

    //public CinemachineDollyCart dollyOne; to delete
   
    public int enemyToDie;
    private bool stopCamera;
    public int waypointNb;
    public int currentWayPoint = 1;

    private bool endGame = false;

    public float[] times;
    public GameObject camChild;

    public PlayableDirector playableDirector;
    private CinemachineTrackedDolly dollyTrack;
    public CinemachineBrain cinemachineBrain;
    public CinemachineVirtualCamera[] virtualCams;
    public CinemachineVirtualCamera activVirtualCam;
    public bool GameIsActiv = false; //If we need to pause the camera move use this

    

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
        waypointNb = wavesList.Count;

        GameObject vcam1 = gameObject.transform.Find("CM vcam1").gameObject;
        activVirtualCam = vcam1.GetComponent<CinemachineVirtualCamera>();
        dollyTrack = activVirtualCam.GetCinemachineComponent<CinemachineTrackedDolly>();
        dollyTrack.m_PathPosition = 0;
        foreach (List<EnemyBehaviour> list in wavesList)
        {
            if(list.Count == 0)
                numberOfenemiesPerWayPoint.Add(0);
            else
            {
                int countEnemies = 0;
                foreach (EnemyBehaviour enemy in list)
                {
                    countEnemies += 1;
                }
                numberOfenemiesPerWayPoint.Add(countEnemies);
            }
        }
        enemyToDie = numberOfenemiesPerWayPoint[0];
        Debug.Log(Camera.main.name);
        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
    }

    // Update is called once per frame
 
    void Update()
    {
        //Debug.Log(cinemachineBrain.ActiveVirtualCamera.Name);
 
        if (cinemachineBrain.ActiveVirtualCamera.Name != activVirtualCam.name)
        {
            string newCam = cinemachineBrain.ActiveVirtualCamera.Name;
            for (int i=0; i< virtualCams.Length; i++)
            {
                if(newCam == virtualCams[i].name)
                {
                    activVirtualCam = virtualCams[i].GetComponent<CinemachineVirtualCamera>();
                    dollyTrack = activVirtualCam.GetCinemachineComponent<CinemachineTrackedDolly>();
                    break;
                }
            }
        }

        if (!GameIsActiv)
            return;

        if (!stopCamera)
        {
            //Debug.Log(currentWayPoint);
            //Debug.Log(dollyTrack.m_PathPosition);
            if(dollyTrack.m_PathPosition >= currentWayPoint)
            {
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
                    enemyToDie = numberOfenemiesPerWayPoint[currentWayPoint-1];
                    stopCamera = false;
                    //dollyOne.m_Speed = 0.5f;
                    playableDirector.Play();
                }
            }
        }
        if(numberOfenemiesPerWayPoint.Count == currentWayPoint && !endGame)
        {
            endGame = true;
            GameManager.instance.GetResults();
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
