using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour {

    public GameObject coinButton;

    IEnumerator displayCoinPanel(float time)
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

    void SpendCoins(float time)
    {
        StartCoroutine("displayCoinPanel");
    }
}
