using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LaserManager : MonoBehaviour
{
    [Tooltip ("Contrôl d'un laser à la souris")]
    public bool DEBUGMODE;
    public static LaserManager instance;
    public Camera mainCamera;
    public GameObject[] laserGameObjects;
    [SerializeField] LaserBehaviour[] lasers;

    [Range(1,4)]public  int playingPlayers = 4;
 
    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            LaserBehaviour.laserManager = this;
            instance = this;
        }
    }
    private void Start()
    {
        mainCamera = Camera.main;

        if (DEBUGMODE)
        {
            playingPlayers = 1;
        }
            

        for (int laserNumber = 1; laserNumber <= 4; laserNumber++)
        {
            if (laserNumber > playingPlayers)
                laserGameObjects[laserNumber-1].SetActive(false);
        }

        lasers = new LaserBehaviour[playingPlayers];
        for (int i = 0; i < playingPlayers; i++)
        {   
            lasers[i] = laserGameObjects[i].GetComponent<LaserBehaviour>();
            lasers[i].UpdateLaserRootPosition();
        }

        

        //TODO WHEN 4 PLAYERS
        /*for(int i = playingPlayers; i < 4; i++)
        {
            laserGameObjects[i].SetActive(false);
        }*/

    }

    /*public void UpdateLaserRootPosition(int _laserArray)
    {
        Ray ray;
        RaycastHit hit;
        ray = mainCamera.ScreenPointToRay(new Vector3((mainCamera.pixelWidth / (playingPlayers + 1)) * (_laserArray + 1), 0, 0));
        lasers[_laserArray].SetPosition(0, ray.origin); 
        //transform.GetChild(_laserArray).position = ray.origin;
    }*/


    void OnDestroy()
    {
        if(instance == this)
        {
            LaserBehaviour.laserManager = null;
            instance = null;
        }
    }
}

