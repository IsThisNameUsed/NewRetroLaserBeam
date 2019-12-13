using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Player : MonoBehaviour {
    [Header("References")]
    [Tooltip("laser should be in Camera children")]
    public LaserBehaviour laser;
    public GameObject targetingSprite;
    [Tooltip("healthBar should be in UICanvas children")]
    public Slider healthBar;
    public Animator animator;
    public GameObject body;
    public Item possesseditem;
    public float numberOfRevive;
    [SerializeField] private int coins;
    [SerializeField] private float damageMultiplier = 1;  //used by item burstDps

    [Header("Players Health")]//on peut vérifier la vie à chaque fois que celle ci est changé.
    [ReadOnly] [SerializeField] public int _playerCurrentHealth;
    [ReadOnly] [SerializeField] bool _playerIsAlive = true;
    //la vie qu'il a avec les coins? Servira pour les revives.
    public int playerCurrentMaxHealth;
    public int playerBonusOnHealth = 0;
    public float playerBonusOnScore = 0;
    [ReadOnly] [SerializeField] private int playerCombo = 0;
    [ReadOnly] [SerializeField] private int playerKill = 0;
    [ReadOnly] [SerializeField] private int playerAssist = 0;
    [ReadOnly] [SerializeField] public float playerDamage = 1;
    [ReadOnly] [SerializeField] private float playerTotalDamage = 0;
    [ReadOnly] [SerializeField] private float playerTotalDamageOnBoss = 0;
    [ReadOnly] [SerializeField] private int playerHitsOnHead = 0;
    [ReadOnly] [SerializeField] private int playerRevivedTeammates = 0;
    [ReadOnly] [SerializeField] private int playerReviveSelf = 0;
    [ReadOnly] [SerializeField] private int playerUsedItem = 0;
    [ReadOnly] [SerializeField] private int playerCollectedCoins = 0;

    [Space(4)]
    [ReadOnly] public float playerScore = 0;
    [ReadOnly] public float playerHitCurrentCooldown = 0;
    [ReadOnly] public EnemyBehaviour.LaserType laserType = EnemyBehaviour.LaserType.Null;
    [SerializeField] private Gradient typeAGradient;
    [SerializeField] private Gradient typeBGradient;

    void Start () {
        playerCurrentMaxHealth = GameManager.instance.playersBaseHealth + playerBonusOnHealth;
        playerCurrentHealth = playerCurrentMaxHealth;
        coins = GameManager.instance.playersBaseCoin;
        if (laser != null)
        {
            laser.UpdateLaserRootPosition();
            switch (laserType)
            {
                case EnemyBehaviour.LaserType.TypeA:
                    laser.GetComponent<LineRenderer>().colorGradient = typeAGradient;
                    break;
                case EnemyBehaviour.LaserType.TypeB:
                    laser.GetComponent<LineRenderer>().colorGradient = typeBGradient;
                    break;
            }

        }
        if(healthBar != null) { animator = healthBar.GetComponent<Animator>(); }
#if UNITY_EDITOR
        Debug.Assert(healthBar != null, "Pas de healthBar attaché à " + this.name);
        Debug.Assert(laser != null, "Pas de laser attaché à " + this.name);
#endif
    }

    private void Update()
    {
        if (playerHitCurrentCooldown > 0)
        {
            playerHitCurrentCooldown -= Time.deltaTime;
        }
    }

    public void UseItem()
    {
        possesseditem.UseItem(this);
        playerUsedItem++;
    }

    public void SetMultiplierDamage(float value)
    {
        damageMultiplier = value;
    }


#region Life

    public bool playerIsAlive
    {
        get { return _playerIsAlive; }
        set { _playerIsAlive = value;
            GameManager.instance.CheckPlayerState();
        }
    }

    public int playerCurrentHealth
    {
        get { return _playerCurrentHealth; }
        set { _playerCurrentHealth = value;
            if (healthBar) { DOTweenModuleUI.DOValue(healthBar, (float)value / playerCurrentMaxHealth, 0.1f); }

            switch (playerIsAlive)
            {
                case true://DEATH
                    if (_playerCurrentHealth <= 0){
                        KillPlayer();
                    }
                    break;
                case false://When player is alive
                    if (_playerCurrentHealth > 0){playerIsAlive = true;}
                    break;
            }
        }
    }

    public int TakeDamage(ref int _damage)
    {
        if (animator != null)
        {
            animator.SetTrigger("isTakingDamage");
        }
        else
        {
            Debug.Log("ANIMATOR NON TROUVE");
        }
        return playerCurrentHealth -= _damage;
    }
   
    public void DebugTakeDamage()
    {
        int damage = 1;
        TakeDamage(ref damage);
    }

    private void OnEnable()
    {
        if(healthBar){ healthBar.gameObject.SetActive(true); }
        if (laser != null) {
            laser.gameObject.SetActive(true);
            laser.SetPlayer(this);
        }

    }

    private void OnDisable()
    {
        if (healthBar) { healthBar.gameObject.SetActive(false); }
        if(laser != null)
        {
            laser.gameObject.SetActive(false);
        }
    }
    #endregion
#region Score
    public void AddKillScore()
    {
        playerKill++;
        playerScore += GameManager.instance.enemyKill_ScoreValue;
    }
    public void AddAssistScore()
    {
        playerAssist++;
        playerScore += GameManager.instance.enemyAssist_ScoreValue;
    }
    public void AddUniqueCoinScore()
    {
        if(coins > 0)
        {
            coins--;
            playerScore += GameManager.instance.coinUnit_ScoreValue;
        }
    }
    public void AddCoinScore()
    {
        while(coins != 0)
        {
            AddUniqueCoinScore();
        }
    }

    private void KillPlayer()
    {
        playerIsAlive = false;
        laser.gameObject.SetActive(false);
        targetingSprite.SetActive(false);
        AddDeathScore();
    }
    public void RevivePlayerSelf()
    {
        playerReviveSelf++;
        RevivePlayer();
    }
    public void ReviveOtherPlayer(int playerIdToRevive)
    {
        playerRevivedTeammates++;
        GameManager.instance.players[playerIdToRevive].RevivePlayer();
    }
    private void RevivePlayer()
    {
        playerCurrentHealth = playerCurrentMaxHealth;
        playerIsAlive = true;
        laser.gameObject.SetActive(true);
        targetingSprite.SetActive(true);
    }
    public void AddDeathScore()
    {
        playerIsAlive = false;
        playerScore -= GameManager.instance.death_ScoreValue;
    }
    public float AddDamageScore(int _multiplier = 1)
    {
        return playerScore += GameManager.instance.damage_ScoreValue * _multiplier;
    }
    public float AddHeadDamageScore(int _multiplier = 1)
    {
        return playerScore += GameManager.instance.damageHead_ScoreValue * _multiplier;
    }
    public int AddHeadHitScore()
    {
        return playerHitsOnHead++;
    }
    public int GetPlayerHeadPopper()
    {
        return playerHitsOnHead;
    }
    public int GetPlayerTeamSpirit()
    {
        return playerRevivedTeammates;
    }
    public int GetPlayerManDown()
    {
        return playerReviveSelf;
    }
    public int GetPlayerEasyMoney()
    {
        return playerCollectedCoins;
    }
    public float GetPlayerTheSlayer()
    {
        return playerTotalDamage;
    }
    public float GetPlayerDominator()//Not developped yet 
    {
        return playerTotalDamageOnBoss;
    }
    public int GetPlayerMacGyver()
    {
        return playerUsedItem;
    }
    #endregion
    #region coins
    public int GetCoins()
    {
        return coins;
    }
    public  int ResetCoin()
    {
        return coins = 0;
    }
    public void AddCoins(int value)
    {
        playerCollectedCoins += value;
        coins += value;
    }
#endregion
}
