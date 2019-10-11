using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyWiFi.Core;

public class LaserBehaviour : MonoBehaviour
{

    public GameObject scopeImage;
    LineRenderer laser;
    public Ray ray;
    public RaycastHit hit;
    public static LaserManager laserManager;
    public bool isShooting = false;
    [Range(0, 3)] public int playerId;
    void Awake()
    {
        laser = GetComponent<LineRenderer>();
    }
    private void Start()
    {
        //SetLaserActive();
    }
    // Update is called once per frame
    void Update()
    {
        UpdateLaserRootPosition();
        UpdateLaserPositions();
#if UNITY_EDITOR
        /*if (playerId == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SetLaserActive(!isShooting);
            }
            Vector3 vector = laserManager.mainCamera.ScreenPointToRay(Input.mousePosition).direction;

            scopeImage.transform.position = laserManager.mainCamera.transform.position + vector.normalized;
        } */
#endif
    }

    public void UpdateLaserRootPosition()
    {
        ray = laserManager.mainCamera.ScreenPointToRay(new Vector3((laserManager.mainCamera.pixelWidth / (LaserManager.playingPlayers + 1)) * (playerId + 1), 0, 0));
        laser.SetPosition(0, ray.origin);
        //transform.GetChild(_laserArray).position = ray.origin;
    }
    public void UpdateLaserPositions()
    {
        ray = laserManager.mainCamera.ScreenPointToRay(laserManager.mainCamera.WorldToScreenPoint(scopeImage.transform.position));
        if (Physics.Raycast(ray, out hit))
        {
            laser.SetPosition(1, hit.point);
        }
        else
        {
            laser.SetPosition(1, scopeImage.transform.position);
        }
    }

    public void SetLaser(ButtonControllerType shootButton)
    {
        if (shootButton.BUTTON_STATE_IS_PRESSED)
        {
            Debug.Log("IS PRESSED");
            isShooting = true;
            laser.enabled = true;
        }  
        else 
        {
            Debug.Log("IS NOT PRESSED");
            isShooting = false;
            laser.enabled = false;
        }
    }
}

