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
}


