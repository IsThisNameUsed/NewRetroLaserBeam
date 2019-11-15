using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    [Header("References")]
    public LaserBehaviour laser;
    public Slider healthBar;
    public Pickable possessedObject;

    [Header("Players Health")]//on peut vérifier la vie à chaque fois que celle ci est changé.
    [ReadOnly] [SerializeField] public int _playerCurrentHealth;
    [ReadOnly] [SerializeField] bool _playerIsAlive = true;
    //la vie qu'il a avec les coins? Servira pour les revives.
    public int playerCurrentMaxHealth;
    public int playerBonusOnHealth = 0;

    // Use this for initialization
    void Start () {
        playerCurrentMaxHealth = GameManager.instance.playersBaseHealth + playerBonusOnHealth;
        playerCurrentHealth = playerCurrentMaxHealth;
        if(laser != null) {laser.UpdateLaserRootPosition();}
    }
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
                    if (_playerCurrentHealth <= 0){playerIsAlive = false;}
                    break;
                case false://When player is alive
                    if (_playerCurrentHealth > 0){playerIsAlive = true;}
                    break;
            }
        }
    }
    public int TakeDamage(ref int _damage)
    {
        Debug.Log(_damage);
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
    void AddPickableToPlayer(itemType type)
    {
       if(type == itemType.GroupeHeal )
        {
            //possessedObject = GetComponent()
        }
       
    }}
// GroupeHeal, PersonnalHeal, BurstDamage };