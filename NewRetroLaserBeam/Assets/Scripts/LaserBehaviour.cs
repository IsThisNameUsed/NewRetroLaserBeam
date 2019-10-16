using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyWiFi.Core;

public class LaserBehaviour : MonoBehaviour
{
    public enum mode { targeting, damageDealer};
    public mode laserMode;
    public GameObject scopeImage;
    LineRenderer laser;
    public Ray ray;
    public RaycastHit hit;
    public bool laserHit = false;
    public static LaserManager laserManager;
    public bool isShooting = false;
    public float laserDamage = 0.1f;
    [Range(0, 3)] public int playerId;
    private AudioSource laserSound;

    void Awake()
    {
        laser = GetComponent<LineRenderer>();
        laserSound = GetComponent<AudioSource>();
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
       /*if (playerId == 0 && EasyWiFiUtilities.getHighestPlayerNumber() == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SetFalseLaser(!isShooting);
            }
            Vector3 vector = laserManager.mainCamera.ScreenPointToRay(Input.mousePosition).direction;

            scopeImage.transform.position = laserManager.mainCamera.transform.position + vector.normalized;
        }*/
#endif
        if (laserHit && laserMode == mode.damageDealer)
        {
            if (isShooting && hit.transform.GetComponent<EnemyBehaviour>())
            {
                hit.transform.GetComponent<EnemyBehaviour>().DealDamage(laserDamage, hit.collider, playerId);
            }
        }

    }

    public void UpdateLaserRootPosition()
    {
        ray = laserManager.mainCamera.ScreenPointToRay(new Vector3((laserManager.mainCamera.pixelWidth / (LaserManager.playingPlayers + 1)) * (playerId + 1), 0, -2));
        laser.SetPosition(0, ray.origin);
        //transform.GetChild(_laserArray).position = ray.origin;
    }
    public void UpdateLaserPositions()
    {
        ray = laserManager.mainCamera.ScreenPointToRay(laserManager.mainCamera.WorldToScreenPoint(scopeImage.transform.position));
        if (Physics.Raycast(ray, out hit))
        {
            laserHit = true;
            laser.SetPosition(1, hit.point);
        }
        else
        {
            laserHit = false;
            laser.SetPosition(1, scopeImage.transform.position);
        }
    }

    public bool SetFalseLaser(bool _state)
    {
        isShooting = _state;
        return laser.enabled = _state;
    }

    public void SetLaser(ButtonControllerType shootButton)
    {
        if (laserMode == mode.targeting)
            return;

        if (shootButton.BUTTON_STATE_IS_PRESSED && isShooting==false)
        {
            //Debug.Log("Switch IS PRESSED");
            laserSound.Play();
            isShooting = true;
            laser.enabled = true;
        }  
        else if(!shootButton.BUTTON_STATE_IS_PRESSED && isShooting == true) 
        {
            //Debug.Log("switch IS NOT PRESSED");
            laserSound.Stop();
            isShooting = false;
            laser.enabled = false;
        }
    }
}

