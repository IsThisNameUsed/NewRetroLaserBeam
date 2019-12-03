using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupHeal : Item {

    public int amountOfLife;
    public Player[] players;
    // Use this for initialization
    void Start () {
        amountOfLife = TweakingItem.instance.groupHealAmountOfLife;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void UseItem(Player player)
    {
        Debug.Log("Utilisation de " + name + " par " + player.gameObject.name);
    }
}
