using EasyWiFi.ClientBackchannels;
using EasyWiFi.ClientControls;
using EasyWiFi.Core;
using EasyWiFi.ServerBackchannels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientManager : MonoBehaviour {

    public GameObject coinButton;
    float time;
    public Text debugText;
    public Text sellText2;
    public GameObject spendCoinsPanel;
    public GameObject gamePanel;

    public GameObject buyButton;
    private Image buyButtonImage;
    public Sprite[] itemSprite;
    private int itemForSaleID;
    public StringDataClientController testString;

    void Start () {
        buyButtonImage = buyButton.transform.GetChild(0).GetComponent<Image>();
        gamePanel.SetActive(false);
        spendCoinsPanel.SetActive(true);
        buyButton.SetActive(false);
        testString.setValue("BUTTON 1");
    }
	
	// Update is called once per frame
	void Update () {
        debugText.text = time.ToString();
    }

    void switchToGameInterface(BoolBackchannelType value)
    {
        gamePanel.SetActive(value.BOOL_VALUE);
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

    public void setString1()
    {
        testString.setValue("BUTTON 1");
    }

    public void setString2()
    {
        testString.setValue("BUTTON 2");
    }

   /*
    //control name: nameObjectForSell
    void setObjectName(StringBackchannelType value)
    {
        sellText.text = value.STRING_VALUE;
    } 
    */
}
