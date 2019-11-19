using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenTaskManager()
    {
        Application.OpenURL("https://docs.google.com/spreadsheets/d/1O1-6kx8uRqei997-1yHI40qxqr8nNpYvcVtusWVxOkU/edit#gid=0");
    }
}
