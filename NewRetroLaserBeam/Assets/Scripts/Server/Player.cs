using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Pickable possessedObject;
    // Use this for initialization

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void AddPickableToPlayer(itemType type)
    {
       if(type == itemType.GroupeHeal )
        {
            //possessedObject = GetComponent()
        }
       
    }
}
// GroupeHeal, PersonnalHeal, BurstDamage };