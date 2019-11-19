using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    

	// Use this for initialization
	void Start () {
        CamManager.instance.wavesList = new List<List<EnemyBehaviour>>();
        Debug.Log("ALLO");
        Debug.Log(transform.childCount);
        foreach (Transform child in transform)
        {
            Debug.Log("FOCK");
            Wave wave = child.gameObject.GetComponent<Wave>();
            CamManager.instance.wavesList.Add(wave.enemyList);
            child.gameObject.SetActive(false);
        }
            
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
