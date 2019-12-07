using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstDamage : Item {

    public float damageMultiplier;
    public float timeOfUse;

    IEnumerator BurstDamageTimer(Player player)
    {
        player.SetMultiplierDamage(damageMultiplier);
        yield return new WaitForSeconds(timeOfUse);
        player.SetMultiplierDamage(1);
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
        StartCoroutine(BurstDamageTimer(player));

    }
}
