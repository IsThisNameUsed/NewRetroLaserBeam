using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalHeal : Item {

    public int amountOfLife;
	// Use this for initialization
	void Start () {
        amountOfLife = TweakingItem.instance.personnalHealAmountOfLife;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void UseItem(Player player)
    {
        Debug.Log("Utilisation de " + name + " par " + player.gameObject.name);
        player._playerCurrentHealth += amountOfLife;
    }
}
