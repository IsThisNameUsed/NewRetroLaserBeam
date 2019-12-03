using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstDamage : Item {

    public float damageMultiplier;

	// Use this for initialization
	void Start () {
        damageMultiplier = TweakingItem.instance.burstDamageMultiplier;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void UseItem(Player player)
    {
        Debug.Log("Utilisation de " + name+" par " + player.gameObject.name);

    }
}
