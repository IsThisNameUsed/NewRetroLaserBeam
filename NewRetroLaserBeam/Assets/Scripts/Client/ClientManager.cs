using EasyWiFi.ClientBackchannels;
using EasyWiFi.Core;
using EasyWiFi.ServerBackchannels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientManager : MonoBehaviour {

    public GameObject coinButton;
    float time;
    public Text text;
    IEnumerator displayCoinPanel()
    {
        yield return new WaitForSeconds(time);
        coinButton.SetActive(false);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        text.text = time.ToString();

    }

    void SpendCoins(FloatBackchannelType inputTime)
    {
        coinButton.SetActive(true);
        time = inputTime.FLOAT_VALUE;
        StartCoroutine("displayCoinPanel");
    }
}
