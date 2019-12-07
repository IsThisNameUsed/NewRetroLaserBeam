using EasyWiFi.Core;
using EasyWiFi.ServerBackchannels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Store : MonoBehaviour {

    public static Store instance;
    [SerializeField]
    private GameObject sellPanel;
    public RawImage itemForSaleImage;
    public Text saleText;
    public Item[] itemsForSell;
    [Tooltip ("must be ordered in the same order than itemForSell!" )]
    public Texture[] itemSprite;

    public Steering steering;                                   //Script use to send information to clients in basics types

    private bool itemSold;                                      // Security bool: Item is bought by a player and can't be bought again
    private int actualSellingItemID;
    private bool sequenceIsStarted = false;
 
    IEnumerator SaleItem()
    {
        SetSaleState(true);
        steering.sellIsActiv(true);
        yield return new WaitForSeconds(5);
        SetSaleState(false);
        Debug.Log("Sell is stop");
        yield return new WaitForSeconds(2);
        itemSold = false;
    }

    IEnumerator StopSell()
    {
        Debug.Log("Sell is stop by a buyer");
        SetSaleState(false);
        yield return new WaitForSeconds(2);
        itemSold = false;
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);


    }

    void Update() {
       
    }

    public void CreateNewSale()
    {
        Debug.Log("Sell is activ");
        actualSellingItemID = Random.Range(0, 3);
        string name = itemsForSell[actualSellingItemID].name;

        steering.sendNameObjectForSale(itemsForSell[actualSellingItemID].name);
        StartCoroutine("SaleItem");          
    }
    
    public void BuyObject(ButtonControllerType buyButton)
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
                SetSaleState(false);
                steering.sendNameObjectForSale("");
                buyer.AddCoins(-itemCost);
                buyer.possesseditem = itemsForSell[actualSellingItemID];
                StopCoroutine("SellItem");
                StartCoroutine("StopSell");
            }    
        }
        
    }

    private void SetSaleState(bool value)
    {
        steering.sellIsActiv(value);
        
        string itemName="";
        if (value)
        {
            itemName = itemsForSell[actualSellingItemID].name;
            sellPanel.SetActive(value);
            saleText.text = itemName + " à vendre!";
            itemForSaleImage.texture = itemSprite[actualSellingItemID];
        }
        else
        {
            sellPanel.SetActive(value);
        }
        steering.sendNameObjectForSale(itemName);
    }
    
}

