using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSession : MonoBehaviour {

    public float time;
    public bool done;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time >= 2 && done == false)
        {
            StartCoroutine(GameManager.instance.GetGameOver(2));
            done = true;
        }
	}
}
