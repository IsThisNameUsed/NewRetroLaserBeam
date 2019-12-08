using EasyWiFi.ClientControls;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberOfCoinDisplayer : MonoBehaviour {

    public Text numberfCoinText;
    private int numberOfCoin;
    public ClientManager clientManager;

    public ButtonClientController healthButton;
    public ButtonClientController scoreButton;

    void Start()
    {
        numberOfCoin = clientManager.startingNumberOfCoin;
        numberfCoinText.text = numberOfCoin.ToString();
    }

    public void DecreaseNumberOfCoin()
    {
        numberOfCoin -= 1;
        numberfCoinText.text = numberOfCoin.ToString();

        if(numberOfCoin <= 0)
        {
            healthButton.DeactivateButton();

            scoreButton.DeactivateButton();
        }
    }
}
