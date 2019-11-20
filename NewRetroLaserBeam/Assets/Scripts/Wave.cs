using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

    public List<EnemyBehaviour> enemyList = new List<EnemyBehaviour>();

	// Use this for initialization
	void Awake () {

        enemyList.AddRange(transform.GetComponentsInChildren<EnemyBehaviour>(true));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

}
