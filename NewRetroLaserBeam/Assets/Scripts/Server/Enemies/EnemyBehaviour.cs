using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
abstract public class EnemyBehaviour : MonoBehaviour {

    public enum LaserType
    {
        Null,
        TypeA,
        TypeB
    }
    protected Animator animator;
    Collider headCollider;
    Collider bodyCollider;
    public ParticleSystem particleSystem;
    Vector3 direction;
    [Space(4)]
    [Header("Enemy Dmg Received")]
    [Space(4)]
    [ReadOnly] public int[] playersHit = { 0, 0, 0, 0 };
    [ReadOnly] public float[] playersTimeHit = { 0, 0, 0, 0 };
    [ReadOnly] public float[] playersDamage = { 0, 0, 0, 0 };
    [ReadOnly] public float[] playersTotalTimeHit = { 0, 0, 0, 0 };
    [ReadOnly] public float[] playersTotalDamage = { 0, 0, 0, 0 };
    [Space(4)]
    [ReadOnly] public bool isCloseToPlayers;
    public Player targetedPlayer;
    [Space(8)]
    [Header("Enemy Stats")]
    [Space(4)]
    [ReadOnly]public float hitCooldown = 0;
    public LaserType weakness = LaserType.Null;
    public bool generateRandomWeakness = false;

    [SerializeField] float healthPoint = 5;
    public float moveSpeed = 0.25f;
    [Range(0, 20)] public float stopDistance = 2;
    public int damagePoint = 1;

    [Range(1, 10)] public int enemyWeaknessDamageMultiplier = 2;
    [Range(1, 10)] public int enemyHeadDamageMultiplier = 2;
    [Range(1, 10)] public int multiLaserDamageMultiplier = 2;

    protected virtual void Start()
    {
        Instanciation();
    }

    protected virtual void Update()
    {
        if (hitCooldown > 0)
        {
            hitCooldown -= Time.deltaTime;
            direction = GameManager.instance.camera.transform.position - transform.position;
            transform.forward = direction.normalized;
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
        particleSystem = transform.GetChild(0).GetComponent<ParticleSystem>();

        headCollider = transform.GetChild(0).GetChild(0).GetComponent<Collider>();
        bodyCollider = transform.GetChild(0).GetChild(1).GetComponent<Collider>();

        EnemyIsActive(true);
        hitCooldown = GameManager.instance.timeToCheckHitOnEnnemy;

        targetedPlayer = GameManager.instance.players[Random.Range(0, GameManager.instance.playingPlayers - 1)];
        if (generateRandomWeakness)
        {
            weakness = (LaserType)Random.Range(0,3);
        }
        switch (weakness)
        {
            case LaserType.TypeA:
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.yellow;
                break;
            case LaserType.TypeB:
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.blue;
                break;
        }
    }

    protected void CheckHealth()
    {
        if (healthPoint <= 0)
        {
            //killer will be the player with the most damage
            //we will give assist to anyone who touched it
            for(int i = 0; i < playersHit.Length; i++)
            {
                if(playersTotalDamage[i] == playersTotalDamage.Max())
                {
                    GameManager.instance.players[i].playerKill++;
                }
                else if(playersTotalDamage[i] > 0)
                {
                    GameManager.instance.players[i].playerAssist++;
                }
            }
            Destroy(gameObject);
            CamManager.instance.DestroyEnemy();
            DamageOnPlayerManager.instance.deleteAttackingEnemy(this);
        }
    }

    public void DealDamage(Collider _col ,int _playerId)
    {
        //Check if hit on head.
        if (_col == headCollider)
        {
            playersHit[_playerId] = 2;
        }
        else
        {
            playersHit[_playerId] = 1;
        }
        playersTimeHit[_playerId] += Time.deltaTime;
    }
    protected float CheckDamage()
    {
        float totalDamage = 0;
        int playerHitting = 0;
        for (int i = 0; i < playersHit.Length; i++)
        {
            switch (playersHit[i])
            {
                case 1:
                    if (weakness == GameManager.instance.players[i].laserType)
                    {
                        playersDamage[i] = (GameManager.instance.players[i].playerDamage * playersHit[i] * playersTimeHit[i]) * enemyWeaknessDamageMultiplier;
                    }
                    else
                    {
                        playersDamage[i] = (GameManager.instance.players[i].playerDamage * playersHit[i] * playersTimeHit[i]);
                    }
                    playerHitting++;
                    break;
                case 2:
                    if (weakness == GameManager.instance.players[i].laserType)
                    {
                        playersDamage[i] = (GameManager.instance.players[i].playerDamage * playersHit[i] * playersTimeHit[i]) * enemyHeadDamageMultiplier * enemyWeaknessDamageMultiplier;
                    }
                    else
                    {
                        playersDamage[i] = (GameManager.instance.players[i].playerDamage * playersHit[i] * playersTimeHit[i]) * enemyHeadDamageMultiplier;
                    }
                    playerHitting++;
                    break;
                default:
                    break;
            }
        }
        for (int i = 0; i < playersHit.Length; i++)
        {
            if(playerHitting > 1)
            {
                playersDamage[i] *= multiLaserDamageMultiplier;
                totalDamage += playersDamage[i];
            }
            else
            {
                totalDamage += playersDamage[i];
            }
            playersTotalDamage[i] += playersDamage[i];
        }
        if (totalDamage > 0)
        {
            particleSystem.Play();
        }

        //Debug.Log(totalDamage + " => "+ playersHit[0] + ", " + playersHit[1] + ", " + playersHit[2] + ", " + playersHit[3]);
        hitCooldown = GameManager.instance.timeToCheckHitOnEnnemy;

        ResetPlayersHit();
        return healthPoint -= totalDamage;
    }

    private void ResetPlayersHit()
    {
        for (int i = 0; i < playersHit.Length; i++)
        {
            playersTotalTimeHit[i] += playersTimeHit[i];
            playersTotalDamage[i] += playersDamage[i];
            playersDamage[i] = 0;
            playersHit[i] = 0;
            playersTimeHit[i] = 0;
        }
    }

    abstract public bool EnemyIsActive(bool _state); 
}
