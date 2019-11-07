using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyWiFi.Core;

public class LaserBehaviour : MonoBehaviour
{
    public enum mode { targeting, damageDealer};
    public mode laserMode;
    LineRenderer laser;
    public Ray ray;
    public RaycastHit hit;
    public bool laserHit = false;
    public static LaserManager laserManager;
    public bool isShooting = false;
    public float laserDamage = 0.2f;
    [Range(0, 3)] public int playerId;
    private Component[] audioSources;
    private AudioSource laserSound;
    public AudioSource laserHitSound;
    public GameObject emitter;
    public GameObject burnParticle;

    public GameObject scope;
    public LayerMask layerMask;

    void Awake()
    {
        laser = GetComponent<LineRenderer>();
        if(laserMode == mode.damageDealer)
        {
            audioSources = GetComponents(typeof(AudioSource));
            laserSound = audioSources[0] as AudioSource;
            laserHitSound = audioSources[1] as AudioSource;
        } 
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

        if (laserHit && laserMode == mode.damageDealer)
        {
            if (isShooting && hit.transform.gameObject.tag == "Enemy")
            {
                hit.transform.GetComponent<EnemyBehaviour>().DealDamage(laserDamage, hit.collider, playerId);
                //burnParticle.SetActive(true);
                //burnParticle.transform.position = hit.point;
            }
        }

//Commande de debug à la souris DEBUG MODE
#if UNITY_EDITOR
        if (LaserManager.instance.debugMode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (laserMode == mode.targeting)
                    return;
                SetLaserDebugMode(!isShooting);
            }
        }
#endif
    }

    public void UpdateLaserRootPosition()
    {
        laser.SetPosition(0, emitter.transform.position);
    }

    public void UpdateLaserPositions()
    {
        ray = new Ray(emitter.transform.position, emitter.transform.forward);

        // ray = laserManager.mainCamera.ScreenPointToRay(laserManager.mainCamera.WorldToScreenPoint(scopeImage.transform.position));
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.tag == "Enemy" && isShooting)
            {
                laserHit = true;
                if (!laserHitSound.isPlaying && laserMode == mode.damageDealer)
                {
                    laserHitSound.Play();
                    laserHitSound.pitch = Random.Range(0.7f, 3);
                }
            }
            else
            {
                laserHit = false;
                if (laserMode == mode.damageDealer)
                {
                    //burnParticle.SetActive(true);
                    laserHitSound.Stop();
                }
            }
            laser.SetPosition(1, hit.point);
        }
        else
        {
            laser.SetPosition(1, emitter.transform.position + emitter.transform.forward * 50);
            laserHit = false;

        }
        
        if(laserMode == mode.targeting)
        {
            Ray scopeRay = new Ray(emitter.transform.position, emitter.transform.forward);
            if (Physics.Raycast(ray, out hit))
            {
               
              
                Vector3 pos = hit.point;
                scope.transform.position = pos;
                
            }
           
        }
        
    }

    public bool SetLaserDebugMode(bool _state)
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

