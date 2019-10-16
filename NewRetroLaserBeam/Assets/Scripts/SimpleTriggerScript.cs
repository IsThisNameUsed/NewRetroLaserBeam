using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTriggerScript : MonoBehaviour {
    public GameObject waveGameObject;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Camera.main.gameObject)
        {
            waveGameObject.SetActive(true);
        }
    }
}
