using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ParticleSystem))]
abstract public class EnemyBehaviour : MonoBehaviour {

    public enum Weakness
    {
        Null,
        TypeA,
        TypeB
    }
    public Weakness weakness = Weakness.Null;
    [SerializeField] float healthPoint = 5;
    public float moveSpeed = 0.25f;
    public int damagePoint = 1;
    [Range(0, 20)] public float stopDistance = 2;
    public bool isCloseToPlayers;

    public int[] playersHit = { 0, 0, 0, 0 };
    public ParticleSystem particleSystem;

    Collider headCollider;
    Collider bodyCollider;
    protected Animator animator;
    public LaserManager laserManager;
    public Player targetedPlayer;

    public float hitCooldown = 0;


    void Start()
    {
        Instanciation();
    }

    void Update()
    {
        if (hitCooldown > 0)
        {
            hitCooldown -= Time.deltaTime;
        }
        else
        {
            CheckDamage();
            CheckHealth(); 
        }
        

    }

    protected void Instanciation()
    {
        animator = GetComponent<Animator>();
        particleSystem = GetComponent<ParticleSystem>();

        headCollider = transform.GetChild(0).GetComponent<Collider>();
        bodyCollider = transform.GetChild(1).GetComponent<Collider>();

        EnemyIsActive(true);
        hitCooldown = GameManager.instance.timeToCheckHitOnEnnemy;

        targetedPlayer = GameManager.instance.players[Random.Range(0, 4)];
        //GameManager.instance.playingPlayers - 1
        weakness = (Weakness)Random.Range(0,2);
    }

    protected void CheckHealth()
    {
        if (healthPoint <= 0)
        {
            Destroy(gameObject);
            CamManager.instance.DestroyEnemy();
            DamageOnPlayerManager.instance.deleteAttackingEnemy(this);
        }
    }

    public float DealDamage(float _damage, Collider _col, int _playerId)
    {
        //print(_damage);
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

            for (int i = 0; i < playersHit.Length; i++)
            {
                totalDamage += (_damage * playersHit[i]);
                Debug.Log(totalDamage);
            }
            ResetPlayersHit();
            hitCooldown = GameManager.instance.timeToCheckHitOnEnnemy;
            print(totalDamage);
            particleSystem.Play();
            return healthPoint -= totalDamage;
        }
        else
        {
            return 0;
        }
    }
    void CheckDamage()
    {

    }

    private void ResetPlayersHit()
    {
        for (int i = 0; i < playersHit.Length; i++)
        {
            playersHit[i] = 0;
        }
    }

    abstract public bool EnemyIsActive(bool _state); 
}
