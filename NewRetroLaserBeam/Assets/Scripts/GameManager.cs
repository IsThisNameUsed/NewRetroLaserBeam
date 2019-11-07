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

    [Range(0, 4)] public int playingPlayers = 4;
    [ReadOnly] public int currentPlayingPlayers = 4;
    private bool allPlayersConnected = false;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else instance = this;

        if (debugMode)
        {
            LaserManager.instance.debugMode = true;
            CamManager.instance.SetGameActiv(true);
            playingPlayers = 0;
            currentPlayingPlayers = playingPlayers;
        }
    }
 
    void Update()
    {
        int numberOfConnectedPlayer = EasyWiFiUtilities.getHighestPlayerNumber()+1;
        if(numberOfConnectedPlayer == playingPlayers && allPlayersConnected == false)
        {
            allPlayersConnected = true;
            this.gameObject.GetComponent<Steering>().time(TimeForSpendCoins);
        }
    }

    public void test(ButtonControllerType button)
    {
        if (button.BUTTON_STATE_IS_PRESSED)
        {
            Debug.Log("INCREASE");
        }
        else if (!button.BUTTON_STATE_IS_PRESSED)
        {

        }
    }


public void stopSpendingCoins(BoolBackchannelType value)
    {
        CamManager.instance.SetGameActiv(value.BOOL_VALUE);
        Debug.Log("GOGOGOGOGO");
    }
}


