using EasyWiFi.Core;
using EasyWiFi.ServerBackchannels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Store en stand by
public class Store : MonoBehaviour {
 
    [SerializeField]
    private GameObject sellPanel;
   
    public Item[] itemsForSell;
    private int actualSellingItemID;
    private bool itemSold;                                      // Security bool: Item is bought by a player and can't be bought again
    
    public Steering steering;                                   //Script use to send information to clients in basics types

    IEnumerator nextObject(int time)
    {
        yield return new WaitForSeconds(time);
        CreateNewPickable();
        //StartNewSequence();
    }

    IEnumerator SellItem()
    {
        SetSellState(true);
        steering.sellIsActiv(true);
        yield return new WaitForSeconds(5);
        SetSellState(false);
        yield return new WaitForSeconds(2);
        itemSold = false;
    }

    IEnumerator StopSell()
    {
        SetSellState(false);
        yield return new WaitForSeconds(2);
        itemSold = false;
    }

    void Start() {

        if (GameManager.instance.AllPlayerAreConnected())
            StartNewSequence();
    }

    // Update is called once per frame
    void Update() {


    }

    void StartNewSequence()
    {
        StartCoroutine(nextObject(5));
    }

    void CreateNewPickable()
    {
        int actualSellingItemID = Random.Range(0, 3);
        string name = itemsForSell[actualSellingItemID].name;
        steering.sendNameObjectForSale(itemsForSell[actualSellingItemID].name);
        StartCoroutine("SellItem");          
    }
    
    void BuyObject(ButtonControllerType buyButton)
    {
        if(buyButton.BUTTON_STATE_IS_PRESSED)
        {
            Debug.Log("CLICK");
            if (itemSold)
                return;

            Debug.Log("Acheté par client numero " + buyButton.clientKey+" " + buyButton.logicalPlayerNumber);
            int playerId = buyButton.logicalPlayerNumber;
            int playersCoins = GameManager.instance.players[playerId].GetCoins();
            int itemCost = itemsForSell[actualSellingItemID].price;
            Debug.Log("Coute " + itemCost + " coins");
            if (playersCoins >= itemCost)
            {
                Player buyer = GameManager.instance.players[playerId];
                itemSold = true;
                SetSellState(false);
                steering.sendNameObjectForSale("");
                buyer.AddCoins(-itemCost);
                buyer.possesseditem = itemsForSell[actualSellingItemID];
                StopCoroutine("SellItem");
                StartCoroutine("StopSell");
            }    
        }
        
    }

    private void SetSellState(bool value)
    {
        steering.sellIsActiv(value);
        
        string itemName="";
        if (value)
        {
            itemName = itemsForSell[actualSellingItemID].name;
            sellPanel.SetActive(value);
            sellPanel.GetComponent<Text>().text = itemName;     
        }
        else
        {
            sellPanel.GetComponent<Text>().text =itemName + " A VENDRE!!!";
            sellPanel.SetActive(value);
        }
        steering.sendNameObjectForSale(itemName);
    }
    
}

