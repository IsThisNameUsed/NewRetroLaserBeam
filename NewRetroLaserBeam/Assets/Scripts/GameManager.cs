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
    private bool allPlayersConnected = false;

    IEnumerator startGameCoroutine()
    {
        yield return new WaitForSeconds(TimeForSpendCoins);
        CamManager.instance.SetGameActiv(true);
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else instance = this;
    }

    private void Start()
    {
        if (debugMode)
        {
            LaserManager.instance.debugMode = true;
            playingPlayers = 0;
        }
            
    }
    void Update()
    {
        int numberOfConnectedPlayer = EasyWiFiUtilities.getHighestPlayerNumber()+1;
        if(numberOfConnectedPlayer == playingPlayers && allPlayersConnected == false)
        {
            allPlayersConnected = true;
            this.gameObject.GetComponent<Steering>().time(TimeForSpendCoins);
            StartCoroutine("startGameCoroutine");
        }
    }

    public void test(ButtonControllerType shootButton)
    {
        if (shootButton.BUTTON_STATE_IS_PRESSED)
        {
            Debug.Log("INCREASE");
        }
        else if (!shootButton.BUTTON_STATE_IS_PRESSED)
        {

        }
    }
}


