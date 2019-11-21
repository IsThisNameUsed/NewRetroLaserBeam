using EasyWiFi.Core;
using EasyWiFi.ServerControls;
using EasyWiFi.ServerBackchannels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Tooltip("Contrôl d'un laser à la souris")]
    public bool debugMode;

    public static GameManager instance;
    public Player[] players;

    [Range(0,4)]public int _playingPlayers = 0;
    public int playingPlayers {
        get { return _playingPlayers; }
        set { _playingPlayers = value;}
    }
    [ReadOnly] public int numberOfConnectedPlayer = 0;
    private bool allPlayersConnected = false;
    private bool gameStart = false;
    private float numberOfReadyPlayers = 0;
    [Tooltip("gameOverPanel should be in UICanvas children")]
    public GameObject gameOverPanel;
    public CustomStringDataServerController test;

    [Space(4)]
    [Tooltip("The number of coin spent on Score")]
    [Header("Editable Variables")]
    [Space(4)]
    [Header("Player Base Stats")]
    [Range(1, 10)] public int playersGlobalScoreIncrementation = 1;
    [Range(1, 10)] public int playersBaseHealth = 3;
    [Range(1, 10)] public int playersBaseCoin = 3;
    [Space(4)]
    [Range(1, 10)] public int playersBonusHealth = 1;
    [Range(1, 10)] public int playersBonusScoreIncrementation = 1;
    [Space(4)]
    [Range(1, 30)] public float TimeForSpendCoins = 5;
    [Range(0.01f, 1)] public float timeToCheckHitOnEnnemy = 0.5f;

    #region ScoreValues
    [Space(8)]
    [Header("Score Parameters")]
    public int enemyKill_ScoreValue = 1500;
    public int enemyAssist_ScoreValue = 700;
    public int coinUnit_ScoreValue = 1000;
    public int death_ScoreValue = -1000;
    public int damage_ScoreValue = 150;
    public int damageCombo_ScoreValue = 150;
    //Je sais pas encore comment on utilise les overtime

    public int death_ScoreValueOvertime = 200;
    public int damageCombo_ScoreValueOvertime = 250;
    #endregion

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else instance = this;
        if (debugMode)
        {
            CamManager.instance.SetGameActiv(true);
            playingPlayers = 0;
            numberOfConnectedPlayer = playingPlayers;
        }
    }

    void Update()
    {
        if (!debugMode)
        {
            numberOfConnectedPlayer = EasyWiFiUtilities.getHighestPlayerNumber() + 1;
            if (numberOfConnectedPlayer == playingPlayers && allPlayersConnected == false && gameStart == false)
            {
                allPlayersConnected = true;
                ActivePlayers(ref _playingPlayers);
                Debug.Log("All players connected");
                gameStart = true;
            }
        }
        else
        {
            playingPlayers = 1;
            ActivePlayers(ref _playingPlayers);
        }
    }

    #region player
    public void playerIsReady(ButtonControllerType button)
    {
        Debug.Log(numberOfReadyPlayers);
        if (button.BUTTON_STATE_IS_PRESSED)
        {
            Debug.Log("one player is ready");
            numberOfReadyPlayers += 1;
        }
        else if (!button.BUTTON_STATE_IS_PRESSED)
        {
            numberOfReadyPlayers -= 1;
        }
        if (numberOfReadyPlayers == playingPlayers)
        {
            CamManager.instance.SetGameActiv(true);
            this.gameObject.GetComponent<Steering>().sendAllPlayerReady();
            Debug.Log("GOGOGOGOGO:" + true);
        }
    }
    public void CheckPlayerState()
    {
        int playerRemaining = 0;
        for (int i = 0; i < playingPlayers; ++i)
        {
            if (players[i].playerIsAlive == true)
            {
                ++playerRemaining;
            }
        }
        if (playerRemaining == 0)
        {
            gameOverPanel.SetActive(true);
            Debug.Log("Game Over");
        }
    }

    public void ActivePlayers(ref int _number)
    {
        int i;
        EnemyBehaviour.LaserType startType = (EnemyBehaviour.LaserType)Random.Range(1, 3);
        for (i=3; i >= playingPlayers; --i)
        {
            players[i].gameObject.SetActive(false);
        }
        for(i=0; i < playingPlayers; ++i)
        {
            players[i].gameObject.SetActive(true);
            if(i == 0)
            {
                players[i].laserType = startType;
            }
            else
            {
                players[i].laserType =  (EnemyBehaviour.LaserType)(Mathf.Repeat((int)startType+i,1)+1);
            }
        }

    }
    #endregion

    public void EndLevel()
    {
        for (int i = 0; i < playingPlayers; ++i)
        {
            AddScore(ref i, coinUnit_ScoreValue * players[i].GetCoins() * players[i].playerBonusOnScore);
            players[i].ResetCoin();
        }
    }
    public float AddScore(ref int _playerId, float _scoreValue = 0)
    {

        return players[_playerId].playerScore += _scoreValue;
    }

    #region get information
    public bool AllPlayerAreConnected()
    {
        return (numberOfConnectedPlayer == playingPlayers);
    }

    #endregion
    #region test
    public void PrintString(StringBackchannelType value)
    {
        Debug.Log(value.STRING_VALUE);
    }

    #endregion
    //Forward channel example
    /*public void stopSpendingCoins(BoolBackchannelType value)
    {
        if (value.BOOL_VALUE == true)
            numberOfReadyPlayers += 1;

        if (numberOfReadyPlayers == playingPlayers)
        {
            CamManager.instance.SetGameActiv(value.BOOL_VALUE);
            Debug.Log("GOGOGOGOGO:" + value.BOOL_VALUE);
        }

    }*/
}
