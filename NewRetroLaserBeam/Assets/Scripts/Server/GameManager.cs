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
    [ReadOnly] public int numberOfConnectedPlayer = 4;
    private bool allPlayersConnected = false;
    private float numberOfReadyPlayers = 0;

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
        } 
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


