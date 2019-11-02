using EasyWiFi.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float TimeForSpendCoins;
    IEnumerator startGameCoroutine()
    {
        yield return new WaitForSeconds(TimeForSpendCoins);
        CamManager.instance.SetGameActiv(true);
    }

    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else instance = this;
        StartCoroutine("startGameCoroutine");
    }

    void Update()
    {

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


