using EasyWiFi.ClientBackchannels;
using EasyWiFi.ClientControls;
using EasyWiFi.Core;
using EasyWiFi.ServerBackchannels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientManager : MonoBehaviour {

    public Text sellText2;
    //Panel
    public GameObject spendCoinsPanel;
    public GameObject gamePanel;

    //Spending coin
    public int startingNumberOfCoin;

    //Sale
    public GameObject buyButton;
    private Image buyButtonImage;
    public Sprite[] itemSprite;
    private int itemForSaleID;


    void Start () {
        buyButtonImage = buyButton.transform.GetChild(0).GetComponent<Image>();
        gamePanel.SetActive(true);
        spendCoinsPanel.SetActive(true);
        buyButton.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

    }

    void switchToGameInterface(BoolBackchannelType value)
    {
        //gamePanel.SetActive(value.BOOL_VALUE);
        spendCoinsPanel.SetActive(!value.BOOL_VALUE);
    }

   

    //control name: sellIsActiv
    void setSellState(IntBackchannelType value)
    {
        if(value.INT_VALUE >= 0)
        {
            buyButton.SetActive(true);         
            buyButtonImage.color = new Vector4(255, 255, 255, 255);
            buyButtonImage.sprite = itemSprite[value.INT_VALUE];
            sellText2.text = "SellIsActiv";
        }
        else
        {
            buyButton.SetActive(false);
            sellText2.text = "SellIsOver";
            buyButtonImage.color = new Vector4(255, 255, 255, 0);
        }  
    }

   /*
    //control name: nameObjectForSell
    void setObjectName(StringBackchannelType value)
    {
        sellText.text = value.STRING_VALUE;
    } 
    */
}
