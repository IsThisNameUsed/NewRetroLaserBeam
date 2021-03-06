﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public GameObject target;
    public Player targetedPlayer;
    public int damage;
    public float speed;
    public bool isActiv;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(isActiv)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "playersCollider")
            targetedPlayer.TakeDamage(ref damage);

        Debug.Log("Collision");
        Destroy(this.gameObject);
    }
}
