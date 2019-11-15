using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyWiFi.Core;

public class LaserBehaviour : MonoBehaviour
{
    LineRenderer laser;
    public EnemyBehaviour enemyHit;
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
    public Slider slider;
    public LayerMask layerMask;
    public GameObject scope;

    [Header("Players Health")]//on peut vérifier la vie à chaque fois que celle ci est changé.
    [ReadOnly] [SerializeField] public int _playerCurrentHealth;
    [ReadOnly] [SerializeField] bool _playerIsAlive = true;
    //la vie qu'il a avec les coins? Servira pour les revives.
    public int playerCurrentMaxHealth;


    void Awake()
    {
        laser = GetComponent<LineRenderer>();
        audioSources = GetComponents(typeof(AudioSource));
        laserSound = audioSources[0] as AudioSource;
        laserHitSound = audioSources[1] as AudioSource;

    }

    private void Start()
    {
        //SetLaserActive();        
        
        playerCurrentHealth = laserManager.playersBaseHealth/* +  coinOnHealth*/;//a changer surement
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLaserRootPosition();
        UpdateLaserPositions();

        if (laserHit)
        {
            if (enemyHit != null && isShooting && enemyHit.transform.gameObject.tag == "Enemy")
            {
                enemyHit.DealDamage(laserDamage, enemyHit.GetComponent<Collider>(), playerId);
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
                SetLaserDebugMode(!isShooting);
            }
        }
#endif
    }
    public int playerCurrentHealth
    {
        get { return _playerCurrentHealth; }
        set
        {
            _playerCurrentHealth = value;
            slider.value = (float)value / playerCurrentMaxHealth;

            switch (playerIsAlive)
            {
                case true:
                    if (_playerCurrentHealth <= 0)//le joueur est mort
                    {
                        playerIsAlive = false;
                    }
                    break;
                case false:
                    if (_playerCurrentHealth > 0)// le joueur vient de start / revive.
                    {
                        playerIsAlive = true;
                    }
                    break;
            }
        }
    }

    public bool playerIsAlive
    {
        get { return _playerIsAlive; }
        set
        {
            _playerIsAlive = value;
            laserManager.CheckPlayerState();
        }
    }

    public void UpdateLaserRootPosition()
    {
        laser.SetPosition(0, emitter.transform.position);
    }

    public void UpdateLaserPositions()
    {
        //On projette un sprite sur un collider avec un ray emis de l'emitter controlé par le gyro
        RaycastHit[] hits = Physics.RaycastAll(emitter.transform.position, emitter.transform.forward, 500.0f);
        GameObject GameObjectHit;
        Vector3 hitPosition = new Vector3(0, 0, 0);
        //ce cas ne devrait pas se présenter le raycast devrait toujours toucher le targetingCollider
        if (hits.Length == 0)
        {
            laser.SetPosition(1, emitter.transform.position + emitter.transform.forward * 100);
            return;
        }
        else
        {
            //On cherche le point d'impact sur le targetingCollider et on translate en coordonnée écran pour positionner le viseur sur le canvas
            for (int i = 0; i < hits.Length; i++)
            {      
                GameObjectHit = hits[i].transform.gameObject;
                if (GameObjectHit.name == "TargetingCollider")
                {
                    Vector3 pos =hits[i].point;
                    pos = Camera.main.WorldToScreenPoint(pos);
                    pos = new Vector3(pos.x, pos.y, pos.z);
                    scope.transform.position = pos;
                    laser.SetPosition(1, hits[i].point);
                    break;
                }
                // TargetingCollider non trouvé -> ce cas ne doit pas arriver
                else if (i == hits.Length - 1)
                {
                    laser.SetPosition(1, emitter.transform.position + emitter.transform.forward * 100);
                }
            } 
            
            //On cast un ray depuis la camera vers le viseur pour détecter si un enemi est dans la ligne de tir
            Ray ray = Camera.main.ScreenPointToRay(scope.transform.position);
            Debug.DrawRay(ray.origin, ray.direction*100, Color.yellow,0.5f);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit,500f,layerMask))
            {
                Transform transformHit = hit.transform;
                if (transformHit.gameObject.tag == "Pickable")
                {
                    Pickable pickable = transformHit.gameObject.GetComponent<Pickable>();
                    return;
                }
                if (transformHit.gameObject.tag == "Enemy")
                {
                    laserHit = true;
                    enemyHit = transformHit.gameObject.GetComponent<EnemyBehaviour>();
                }
                else laserHit = false;
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
        if (shootButton.BUTTON_STATE_IS_PRESSED && isShooting == false)
        {
            //Debug.Log("Switch IS PRESSED");
            laserSound.Play();
            isShooting = true;
            laser.enabled = true;
        }
        else if (!shootButton.BUTTON_STATE_IS_PRESSED && isShooting == true)
        {
            //Debug.Log("switch IS NOT PRESSED");
            laserSound.Stop();
            isShooting = false;
            laser.enabled = false;
        }
    }
    public void DebugTakeDamage()
    {
        int damage = 1;
        TakeDamage(ref damage);
    }
    public int TakeDamage(ref int _damage)
    {
        Debug.Log(_damage);
        return playerCurrentHealth -= _damage;
    }
    private void OnEnable()
    {
        slider.gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        slider.gameObject.SetActive(false);
    }
}
