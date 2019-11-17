using EasyWiFi.Core;
using EasyWiFi.ServerBackchannels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{ 
    [Tooltip("Contrôl d'un laser à la souris")]
    public bool debugMode;

    public static GameManager instance;
    
    public float TimeForSpendCoins;
    [Range(0,4)]
    public int _playingPlayers = 0;

    public int playingPlayers {
        get { return _playingPlayers; }
        set { _playingPlayers = value;
              SetPlayersNumber(ref _playingPlayers);}
    }

    [ReadOnly] public int numberOfConnectedPlayer = 4;
    private bool allPlayersConnected = false;
    private float numberOfReadyPlayers = 0;
    [Range(1,10)]public int playersBaseHealth = 3;
    public Player[] players;
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
        if(!debugMode)
        {
            int numberOfConnectedPlayer = EasyWiFiUtilities.getHighestPlayerNumber() + 1;
            if (numberOfConnectedPlayer == playingPlayers && allPlayersConnected == false)
            {
                allPlayersConnected = true;
            }
            playingPlayers = playingPlayers;
        }
        else playingPlayers = 1;
    }

    public void playerIsReady(ButtonControllerType button)
    {
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
            Debug.Log("Game Over");
        }
    }
    public void SetPlayersNumber(ref int _number)
    {
        int i;
        for (i=3; i >= playingPlayers; --i)
        {
            players[i].gameObject.SetActive(false);
        }
        for(i=0; i < playingPlayers; ++i)
        {
            players[i].gameObject.SetActive(true);
        }

    }
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


