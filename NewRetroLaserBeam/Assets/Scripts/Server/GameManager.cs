using EasyWiFi.Core;
using EasyWiFi.ServerControls;
using EasyWiFi.ServerBackchannels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private float totalCoinSPendingForScore;

    [Space(4)]
    [Tooltip("The number of coin spent on Score")]
    [Header("Editable Variables")]
    [Space(4)]
    [Header("Player Base Stats")]
    [Range(1, 10)] public int playersGlobalScoreIncrementation = 1;
    [Range(1, 10)] public int playersBaseHealth = 10;
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
    public int damage_ScoreValue = 50;
    public int damageHead_ScoreValue = 100;

    [Space(4)]
    [Header("ResultsObjects")]
    public GameObject resultsPanel;
    public Text finalScoreText;

    public GameObject headPopper;
    public GameObject teamSpirit;
    public GameObject manDown;
    public GameObject easyMoney;
    public GameObject theSlayer;
    public GameObject dominator;
    public GameObject macGyver;

    [ReadOnly] public float finalScore;
    
    [ReadOnly] [SerializeField]private string headPopperPlayer;
    private int headPopperValue;
    [ReadOnly] [SerializeField] private string teamSpiritPlayer;
    private int teamSpiritValue;
    [ReadOnly] [SerializeField] private string manDownPlayer;
    private int manDownValue;
    [ReadOnly] [SerializeField] private string easyMoneyPlayer;
    private int easyMoneyValue;
    [ReadOnly] [SerializeField] private string theSlayerPlayer;
    private float theSlayerValue;
    [ReadOnly] [SerializeField] private string dominatorPlayer;
    private float dominatorValue;
    [ReadOnly] [SerializeField] private string macGyverPlayer;
    private int macGyverValue;

    public void GetResults()
    {
        resultsPanel.SetActive(true);
        GetFinalScore();
        GetHeadPopper();
        GetTeamSpirit();
        GetManDown();
        GetEasyMoney();
        GetTheSlayer();
        GetDominator();
        GetMacGyver();

        finalScoreText.text = finalScore.ToString();

        if(headPopperPlayer != "Null")
        {
            headPopper.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = headPopperPlayer + " - " + headPopperValue;
            headPopper.SetActive(true);
        }
        if (teamSpiritPlayer != "Null")
        {
            teamSpirit.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = teamSpiritPlayer + " - " + teamSpiritValue;
            teamSpirit.SetActive(true);
        }
        if (manDownPlayer != "Null")
        {
            manDown.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = manDownPlayer + " - " + manDownValue;
            manDown.SetActive(true);
        }
        if (easyMoneyPlayer != "Null")
        {
            easyMoney.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = easyMoneyPlayer + " - " + easyMoneyValue;
            easyMoney.SetActive(true);
        }
        if (theSlayerPlayer != "Null")
        {
            theSlayer.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = theSlayerPlayer + " - " + theSlayerValue;
            theSlayer.SetActive(true);
        }
        if (dominatorPlayer != "Null")
        {
            dominator.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = dominatorPlayer + " - " + dominatorValue;
            dominator.SetActive(true);
        }
        if (macGyverPlayer != "Null")
        {
            macGyver.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = macGyverPlayer + " - " + macGyverValue;
            macGyver.SetActive(true);
        }
}
    private float GetFinalScore()
    {
        foreach(Player player in players)
        {
            finalScore += player.playerScore;
        }
        return finalScore;
    }
    private string GetHeadPopper()
    {
        int headPopperId = -1;
        for(int i = 0; i < playingPlayers; ++i)
        {
            if(players[i].GetPlayerHeadPopper() != 0)
            {
                if(headPopperId != -1)
                {
                    if (players[headPopperId].GetPlayerHeadPopper() < players[i].GetPlayerHeadPopper())
                    {
                        headPopperId = i;
                    }
                }
                else
                {
                    headPopperId = i;
                }
            }
        }
        if(headPopperId != -1)
        {
            headPopperValue = players[headPopperId].GetPlayerHeadPopper();
        }
        return headPopperPlayer = ReturnPlayer(ref headPopperId);
    }
    private string GetTeamSpirit()
    {
        int teamSpiritId = -1;
        for (int i = 0; i < playingPlayers; ++i)
        {
            if (players[i].GetPlayerTeamSpirit() != 0)
            {
                if (teamSpiritId != -1)
                {
                    if (players[teamSpiritId].GetPlayerTeamSpirit() < players[i].GetPlayerTeamSpirit())
                    {
                        teamSpiritId = i;
                    }
                }
                else
                {
                    teamSpiritId = i;
                }
            }
        }
        if (teamSpiritId != -1)
        {
            teamSpiritValue = players[teamSpiritId].GetPlayerTeamSpirit();
        }
        return teamSpiritPlayer = ReturnPlayer(ref teamSpiritId);
    }
    private string GetManDown()
    {
        int manDownId = -1;
        for (int i = 0; i < playingPlayers; ++i)
        {
            if (players[i].GetPlayerManDown() != 0)
            {
                if (manDownId != -1)
                {
                    if (players[manDownId].GetPlayerManDown() < players[i].GetPlayerManDown())
                    {
                        manDownId = i;
                    }
                }
                else
                {
                    manDownId = i;
                }
            }
        }
        if (manDownId != -1)
        {
            manDownValue = players[manDownId].GetPlayerManDown();
        }
        return manDownPlayer = ReturnPlayer(ref manDownId);
    }
    private string GetEasyMoney()
    {
        int easyMoneyId = -1;
        for (int i = 0; i < playingPlayers; ++i)
        {
            if (players[i].GetPlayerEasyMoney() != 0)
            {
                if (easyMoneyId != -1)
                {
                    if (players[easyMoneyId].GetPlayerEasyMoney() < players[i].GetPlayerEasyMoney())
                    {
                        easyMoneyId = i;
                    }
                }
                else
                {
                    easyMoneyId = i;
                }
            }
        }
        if (easyMoneyId != -1)
        {
            easyMoneyValue = players[easyMoneyId].GetPlayerEasyMoney();
        }
        return easyMoneyPlayer = ReturnPlayer(ref easyMoneyId);
    }
    private string GetTheSlayer()
    {
        int theSlayerId = -1;
        for (int i = 0; i < playingPlayers; ++i)
        {
            if (players[i].GetPlayerTheSlayer() != 0)
            {
                if (theSlayerId != -1)
                {
                    if (players[theSlayerId].GetPlayerTheSlayer() < players[i].GetPlayerTheSlayer())
                    {
                        theSlayerId = i;
                    }
                }
                else
                {
                    theSlayerId = i;
                }
            }
        }
        if (theSlayerId != -1)
        {
            theSlayerValue = players[theSlayerId].GetPlayerTheSlayer();
        }
        return theSlayerPlayer = ReturnPlayer(ref theSlayerId);
    }
    private string GetDominator()
    {
        int dominatorId = -1;
        for (int i = 0; i < playingPlayers; ++i)
        {
            if (players[i].GetPlayerDominator() != 0)
            {
                if (dominatorId != -1)
                {
                    if (players[dominatorId].GetPlayerDominator() < players[i].GetPlayerDominator())
                    {
                        dominatorId = i;
                    }
                }
                else
                {
                    dominatorId = i;
                }
            }
        }
        if (dominatorId != -1)
        {
            dominatorValue = players[dominatorId].GetPlayerDominator();
        }
        return dominatorPlayer = ReturnPlayer(ref dominatorId);
    }
    private string GetMacGyver()
    {
        int macGyverId = -1;
        for (int i = 0; i < playingPlayers; ++i)
        {
            if (players[i].GetPlayerMacGyver() != 0)
            {
                if (macGyverId != -1)
                {
                    if (players[macGyverId].GetPlayerMacGyver() < players[i].GetPlayerMacGyver())
                    {
                        macGyverId = i;
                    }
                }
                else
                {
                    macGyverId = i;
                }
            }
        }
        if (macGyverId != -1)
        {
            macGyverValue = players[macGyverId].GetPlayerMacGyver();
        }
        return macGyverPlayer = ReturnPlayer(ref macGyverId);
    }
    private string ReturnPlayer(ref int playerId)
    {
        switch (playerId)
        {
            case 0:
                return "Player 1";
            case 1:
                return "Player 2";
            case 2:
                return "Player 3";
            case 3:
                return "Player 4";
            default:
                return "Null";
        }
    }
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
            playingPlayers = _playingPlayers;
            numberOfConnectedPlayer = playingPlayers;
        }
    }

    void Update()
    {
        if (!debugMode)
        {
            numberOfConnectedPlayer = EasyWiFiUtilities.getHighestPlayerNumber()+1;
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
            if(gameStart == false)
            {
                //playingPlayers = 1;
                ActivePlayers(ref _playingPlayers);
                gameStart = true;
            }
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

    public void SpendCoinsForHealthP1()
    {
        players[0].numberOfRevive += 0.5f;
    }
    public void SpendCoinsForHealthP2()
    {
        players[1].numberOfRevive += 0.5f;
    }
    public void SpendCoinsForHealthP3()
    {
        players[2].numberOfRevive += 0.5f;
    }
    public void SpendCoinsForHealthP4()
    {
        players[3].numberOfRevive += 0.5f;
    }

    public void SpendCoinForScore()
    {
        totalCoinSPendingForScore += 0.5f;
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
