using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ParticleSystem))]
public class EnemyBehaviour : MonoBehaviour {

    [SerializeField]float healthPoint = 1;
    public float moveSpeed = 0.25f;
    public float damagePoint = 1;
    public bool isCloseToPlayers;
    Collider headCollider;
    Collider bodyCollider;
    public Camera mainCamera;
    Animator animator;
    public float hitTime = 1f;
    public float hitCooldown = 0;
    public int[] playersHit = { 0, 0, 0, 0 };
    public ParticleSystem particleSystem;
    public bool isMoving = true;
    [Range(0,20)]public float stopDistance = 2;

    void Start () {
        mainCamera = Camera.main;
  
        animator = GetComponent<Animator>();
        particleSystem = GetComponent<ParticleSystem>();

        headCollider = transform.GetChild(0).GetComponent<Collider>();
        bodyCollider = transform.GetChild(1).GetComponent<Collider>();

        EnemyIsActive(isMoving);
        hitCooldown = hitTime;
    }
	
	// Update is called once per frame
	void Update () {
        if(hitCooldown > 0)
        {
            hitCooldown -= Time.deltaTime;
        }
        CheckHealth();

	}

    public void GoToLocation()
    {
        transform.position += (mainCamera.transform.position - transform.position).normalized * Time.deltaTime * moveSpeed; 
    }

    void CheckHealth()
    {
        if (healthPoint <= 0)
        {
            Destroy(gameObject);
            CamManager.instance.DestroyEnemy();
            DamageOnPlayerManager.instance.deleteAttackingEnemy(this);
        }
    }

    public float DealDamage(float _damage,Collider _col, int _playerId)
    {
        print(_damage);
        //Check if hit on head.
        if (_col == headCollider)
        {
            playersHit[_playerId] = 2;
            Debug.Log("HIT HEAD");
        }
        else
        {
            playersHit[_playerId] = 1;
            Debug.Log("HIT BODY");
        }
        //Check if we can hit
        if (hitCooldown <= 0)
        {
            float totalDamage = 0;
            
            for(int i = 0; i < playersHit.Length; i++)
            {
                totalDamage += (_damage * playersHit[i]);
                Debug.Log(totalDamage);
            }
            ResetPlayersHit();
            hitCooldown = hitTime;
            print(totalDamage);
            particleSystem.Play();
            return healthPoint -= totalDamage;
        }
        else
        {
            return 0;
        }

    }

    private void ResetPlayersHit()
    {
        for(int i = 0; i < playersHit.Length; i++)
        {
            playersHit[i] = 0;
        }
    }

    public bool EnemyIsActive(bool _state)
    {
        animator.SetBool("isActive", _state);
        return _state;
    }

    public bool EnemyIsClose(bool _state)
    {
        animator.SetBool("isClose", _state);
        return _state;
    }
}
