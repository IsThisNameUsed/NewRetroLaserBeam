using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {


    public GameObject gunsight;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartShoot()
    {
        RaycastHit hit;
        Debug.Log("Shoot");
        if (Physics.Raycast(gunsight.transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, -1))
        {
            Debug.DrawRay(gunsight.transform.position, gunsight.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            Destroy(hit.transform.gameObject);
        }
        else
        {
            Debug.DrawRay(gunsight.transform.position, gunsight.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }
}
