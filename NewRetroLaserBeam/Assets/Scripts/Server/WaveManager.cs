using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    

	// Use this for initialization
	void Awake () {
        CamManager.instance.wavesList = new List<List<EnemyBehaviour>>();
        foreach (Transform child in transform)
        {            
            Wave wave = child.gameObject.GetComponent<Wave>();
            CamManager.instance.wavesList.Add(wave.enemyList); 
            child.gameObject.SetActive(false);
        }
            
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
