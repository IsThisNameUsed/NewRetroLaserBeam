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
    public Text sellText;
    public Text sellText2;
    public GameObject spendCoinsPanel;
    public GameObject gameInterfacePanel;
    //Forward channel example
    /*IEnumerator displayCoinPanel()
    {
        yield return new WaitForSeconds(time);
        coinButton.SetActive(false);
        gameObject.GetComponent<BoolDataClientController>().setValue(true);
    }*/

    private void Awake()
    {
    }
    // Use this for initialization
    void Start () {
        gameInterfacePanel.SetActive(false);
        spendCoinsPanel.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        debugText.text = time.ToString();
    }

    /*void SpendCoins(FloatBackchannelType inputTime)
    {
        coinButton.SetActive(true);
        time = inputTime.FLOAT_VALUE;
        StartCoroutine("displayCoinPanel");
    }*/

    void switchToGameInterface(BoolBackchannelType value)
    {
        gameInterfacePanel.SetActive(value.BOOL_VALUE);
        spendCoinsPanel.SetActive(!value.BOOL_VALUE);
    }

    void StartSell(StringBackchannelType value)
    {
        sellText.text = value.STRING_VALUE;
    }

    void sellIsOver(BoolBackchannelType value)
    {
        if(value.BOOL_VALUE)
            sellText2.text = "SellIsOver";
    }
}
