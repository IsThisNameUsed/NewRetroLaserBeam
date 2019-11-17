using EasyWiFi.Core;
using EasyWiFi.ServerBackchannels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Store en stand by
public class Store : MonoBehaviour {

    public Pickable[] pickable;
    [SerializeField]
    private GameObject groupHealSellPanel;
    [SerializeField]
    private GameObject personalHealSellPanel;
    [SerializeField]
    private GameObject burstDamageSellPanel;

    //Script use to send information to clients in basics types
    public Steering steering;

    IEnumerator nextObject(int time)
    {
        yield return new WaitForSeconds(time);
        CreateNewPickable();
        //StartNewSequence();
    }

    IEnumerator sellDuration()
    {
        Debug.Log("Coroutine sell");
        steering.sellIsActiv(true);
        yield return new WaitForSeconds(5);
        steering.sellIsActiv(false);
        steering.sendNameObjectForSale("");
    }

    void Start() {
        StartNewSequence();
    }

    // Update is called once per frame
    void Update() {


    }

    void StartNewSequence()
    {
        StopAllCoroutines();
        StartCoroutine(nextObject(5));
    }

    void CreateNewPickable()
    {
        int type = Random.Range(1, 3);
        switch(type)
        {
            case 1:
                groupHealSellPanel.SetActive(true);
                steering.sendNameObjectForSale("groupHeal");
                StartCoroutine(sellDuration());
                break;
            case 2:
                personalHealSellPanel.SetActive(true);
                steering.sendNameObjectForSale("personalHeal");
                StartCoroutine(sellDuration());
                break;
            case 3:
                burstDamageSellPanel.SetActive(true);
                steering.sendNameObjectForSale("burstDamage");
                StartCoroutine(sellDuration());
                break;
        }
    }
    
    void BuyObject(ButtonControllerType buyButton)
    {
        if(buyButton.BUTTON_STATE_IS_PRESSED)
        {    
            Debug.Log("Acheté par client numero " + buyButton.clientKey+" " + buyButton.logicalPlayerNumber);
            steering.sellIsActiv(false);
            steering.sendNameObjectForSale("");
        }
        
    }
}
