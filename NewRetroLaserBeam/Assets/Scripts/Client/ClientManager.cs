using EasyWiFi.ClientBackchannels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour {

    public GameObject coinButton;
    float time;
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
		
	}

    void SpendCoins(FloatClientBackchannel time)
    {
        coinButton.SetActive(false);
        StartCoroutine("displayCoinPanel");
    }
}
