using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LaserManager : MonoBehaviour
{
    public static LaserManager instance;
    public Camera mainCamera;
    public GameObject[] laserGameObjects;
    [SerializeField] LaserBehaviour[] lasers;

    [Range(1,4)]public static int playingPlayers = 4;
    [Range(1, 4)] public int _playingPlayers = 4;
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
        playingPlayers = _playingPlayers;
        lasers = new LaserBehaviour[playingPlayers];
        for (int i = 0; i < playingPlayers; i++)
        {
            //laserGameObjects[i] = transform.GetChild(i).gameObject;
                
            lasers[i] = laserGameObjects[i].GetComponent<LaserBehaviour>();
            lasers[i].UpdateLaserRootPosition();
        }
        for(int i = playingPlayers; i < 4; i++)
        {
            laserGameObjects[i].SetActive(false);
        }

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

