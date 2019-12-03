using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweakingItem : MonoBehaviour {

    public static TweakingItem instance;

    public int personnalHealAmountOfLife;
    public int groupHealAmountOfLife;
    public int burstDamageMultiplier;
    public float burstDamageTimeOfUse;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else instance = this;
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
