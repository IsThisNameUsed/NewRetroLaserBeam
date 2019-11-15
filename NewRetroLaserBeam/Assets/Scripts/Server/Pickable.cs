using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour {

    public itemType type;
	void Start () {
        //this.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

public enum itemType { GroupeHeal, PersonalHeal, BurstDamage };
