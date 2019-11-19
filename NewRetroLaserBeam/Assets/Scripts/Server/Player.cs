using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    [Header("References")]
    [Tooltip("laser should be in Camera children")]
    public LaserBehaviour laser;
    [Tooltip("healthBar should be in UICanvas children")]
    public Slider healthBar;
    public Item possesseditem;
    [SerializeField] private int coins;

    [Header("Players Health")]//on peut vérifier la vie à chaque fois que celle ci est changé.
    [ReadOnly] [SerializeField] public int _playerCurrentHealth;
    [ReadOnly] [SerializeField] bool _playerIsAlive = true;
    //la vie qu'il a avec les coins? Servira pour les revives.
    public int playerCurrentMaxHealth;
    public int playerBonusOnHealth = 0;
    public float playerBonusOnScore = 0;
    [ReadOnly] public int playerCombo = 0;
    [ReadOnly] public int playerKill = 0;
    [ReadOnly] public int playerAssist = 0;
    [Space(4)]
    [ReadOnly] public float playerScore = 0;


    void Start () {
        playerCurrentMaxHealth = GameManager.instance.playersBaseHealth + playerBonusOnHealth;
        playerCurrentHealth = playerCurrentMaxHealth;
        coins = GameManager.instance.playersBaseCoin;
        if (laser != null) { laser.UpdateLaserRootPosition(); }
        Debug.Assert(healthBar != null, "Pas de healthBar attaché à " + this.name);
#if UNITY_EDITOR
        Debug.Assert(laser != null, "Pas de laser attaché à " + this.name);
#endif
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
            if (healthBar) {healthBar.value = (float)value / playerCurrentMaxHealth;}
            
            switch (playerIsAlive)
            {
                case true://DEATH
                    if (_playerCurrentHealth <= 0){playerIsAlive = false;
                        playerScore -= GameManager.instance.death_ScoreValue;
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
        coins += value;
    }
#endregion
}
