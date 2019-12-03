using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstDamage : Item {

    public float damageMultiplier;
    public float timeOfUse;

    IEnumerator BusrtDamageTimer()
    {
        yield return new WaitForSeconds(3);
    }
	// Use this for initialization
	void Start () {
        damageMultiplier = TweakingItem.instance.burstDamageMultiplier;
        timeOfUse = TweakingItem.instance.burstDamageTimeOfUse;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void UseItem(Player player)
    {
        Debug.Log("Utilisation de " + name+" par " + player.gameObject.name);
        //player.dam

    }
}
