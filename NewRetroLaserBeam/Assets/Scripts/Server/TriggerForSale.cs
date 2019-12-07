using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerForSale : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Camera.main.gameObject)
        {
            Store.instance.CreateNewSale();
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(transform.position, 2);
    }
}
