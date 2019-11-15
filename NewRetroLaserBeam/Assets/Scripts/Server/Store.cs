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
    [SerializeField]
    private Steering steering;

    IEnumerator nextObject(int time)
    {
        yield return new WaitForSeconds(time);
        CreateNewPickable();
        //StartNewSequence();
    }

    IEnumerator sellDuration()
    {
        Debug.Log("Coroutine sell");
        yield return new WaitForSeconds(5);
        steering.sendSellIsOver(true);
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
        StartCoroutine(nextObject(6));
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
}
