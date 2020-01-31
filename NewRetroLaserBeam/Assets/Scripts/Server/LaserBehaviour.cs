using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyWiFi.Core;
using Rewired;

public class LaserBehaviour : MonoBehaviour
{

    LineRenderer laser;
    public EnemyBehaviour enemyHit;
    public Collider bodyPart;
    public bool laserHit = false;
    public static LaserManager laserManager;
    public bool isShooting = false;
    [Range(0, 3)] public int playerId;
    private Component[] audioSources;
    private AudioSource laserSound;
    public AudioSource laserHitSound;
    public GameObject emitter;
    public GameObject burnParticle;

    public LayerMask layerMask;
    public GameObject scope;
    public Player player;
    public Rewired.Player playerInput;

    [ReadOnly][SerializeField]
    private bool shootRightPressed;
    [ReadOnly][SerializeField]
    private bool shootLeftPressed;
    private float shootStartTime;
    public float shootTimeBeforeOverheat = 3;
    public GameObject overheatImagePrefab;
    [ReadOnly][SerializeField]
    private bool isOverheat;
    public float timeForCooling = 3;
    public Image overheatGauge;
    [Tooltip ("1 for no malus")]
    public float coolingMalusMultiplicator;

    IEnumerator Cooling(float time)
    {
        float timeElapsed = 0;
        float step = time / 100;
        Debug.Log("Colling starting");
        while(overheatGauge.fillAmount > 0)
        {
            yield return new WaitForSeconds(step);
            timeElapsed += step;
            if(isOverheat)
                overheatGauge.fillAmount -= (timeElapsed / time)/coolingMalusMultiplicator;
            else overheatGauge.fillAmount -= (timeElapsed / time);
        }
        
        Debug.Log("Colling ok");
        isOverheat = false;
    }

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
        if(player != null)
        {
            playerInput = ReInput.players.GetPlayer(player.name);
        }
        if(overheatGauge == null)
        {
            GameObject gauge = Instantiate(overheatImagePrefab, scope.transform);
            overheatGauge = gauge.GetComponent<Image>();
        }


    }

    // Update is called once per frame
    void Update()
    {
        UpdateLaserRootPosition();
        UpdateLaserPositions();
        if (playerInput.GetButton("Shoot"))
        {
            shootRightPressed = true;
            shootLeftPressed = true;
        }
        else
        {
            shootLeftPressed = false;
            shootRightPressed = false;
        }
      // if (!GameManager.instance.debugMode)
            SetLaserState();

        if (laserHit)
        {
            if (enemyHit != null && isShooting && enemyHit.transform.gameObject.tag == "Enemy")
            {
                enemyHit.DealDamage(bodyPart ,playerId);
                //burnParticle.SetActive(true);
                //burnParticle.transform.position = hit.point;
            }
        }

#if UNITY_EDITOR//Commande de debug à la souris DEBUG MODE
        /*if (GameManager.instance.debugMode)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SetLaserDebugMode(!isShooting);
            }
        }*/
#endif
    } 

    public Player SetPlayer(Player _player)
    {
        return player = _player;
    }

    public void UpdateLaserRootPosition()
    {
      /*  if(GameManager.instance.playingPlayers > 1)
        {
             transform.position = Camera.main.ScreenPointToRay(new Vector3(Screen.width*playerId/GameManager.instance.playingPlayers, -1, 0)).origin;
        }*/
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
                    //pos = new Vector3(pos.x, pos.y, pos.z);
                    //scope.transform.position = pos;
                    laser.SetPosition(1, hits[i].point);
                    break;
                }
                else if (i == hits.Length - 1)// TargetingCollider non trouvé -> ce cas ne doit pas arriver
                {
                    laser.SetPosition(1, emitter.transform.position + emitter.transform.forward * 100);
                }
            }

            //On cast un ray depuis la camera vers le viseur pour détecter si un enemi est dans la ligne de tir
            Ray ray = Camera.main.ScreenPointToRay(scope.transform.position);
            Debug.DrawRay(ray.origin, ray.direction*100, Color.yellow,0.5f);
            RaycastHit hit;
            if( (player.playerIsAlive || GameManager.instance.debugMode) && 
                     Physics.Raycast(ray, out hit,500f,layerMask))
            {
                Transform transformHit = hit.transform;
                if (transformHit.gameObject.tag == "Pickable")
                {
                    Pickable pickable = transformHit.gameObject.GetComponent<Pickable>();
                    player.AddCoins(1);
                    Destroy(pickable.gameObject);
                    return;
                }
                if (transformHit.gameObject.tag == "Enemy")
                {
                    laserHit = true;
                    enemyHit = transformHit.gameObject.GetComponent<EnemyBehaviour>();
                    bodyPart = hit.collider;
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
    private void BeginShoot()
    {
        isShooting = true;
        shootStartTime = Time.time;
        //Debug.Log("start time = " + shootStartTime);
    }

    private void ContinueShoot()
    {
        //Debug.Log(Time.time - shootStartTime + " compare to " + shootTimeBeforeOverheat);
        float timeElapsed = Time.time - shootStartTime;
        
        if (overheatGauge.fillAmount >= 1)
        {
            isOverheat = true;
            Debug.Log("Overheat");
        }
        overheatGauge.fillAmount += (1 / shootTimeBeforeOverheat)*Time.deltaTime;
    }

    private void SetLaserState()
    {
        if((shootRightPressed || shootLeftPressed) && !isOverheat)
        {
            if (!isShooting)
            {
                BeginShoot();
            }
            else
            {
                ContinueShoot();
                StopAllCoroutines();
                Debug.Log("Stop cooling");
                laserSound.Play();
                isShooting = true;
                laser.enabled = true;
            }
        } 
        else if(isShooting)
        {
            laserSound.Stop();
            StartCoroutine(Cooling(timeForCooling));
            isShooting = false;
            laser.enabled = false;
        }   
    }

    public void ReceiveInputRightShoot(ButtonControllerType shootButton1)
    {
        if (shootButton1.BUTTON_STATE_IS_PRESSED && shootRightPressed == false)
        {
            Debug.Log("Shoot1 IS PRESSED");
            shootRightPressed = true;


        }
        else if (!shootButton1.BUTTON_STATE_IS_PRESSED && isShooting == true)
        {
            shootRightPressed = false;
        }
    }

    public void ReceiveInputLeftShoot(ButtonControllerType shootButton2)
    {
        if (shootButton2.BUTTON_STATE_IS_PRESSED && shootLeftPressed == false)
        {
            Debug.Log("Shoot2 IS PRESSED");
            shootLeftPressed = true;


        }
        else if (!shootButton2.BUTTON_STATE_IS_PRESSED && isShooting == true)
        {
            shootLeftPressed = false;
        }
    }
}
