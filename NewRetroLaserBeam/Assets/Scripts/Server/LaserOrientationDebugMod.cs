using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
public class LaserOrientationDebugMod : MonoBehaviour {

    Camera cam;

    public GameObject scopeImage;
    public Transform lineRendererTarget;
    public float rotationSpeed = 8;
    LaserBehaviour player;
    // Use this for initialization
    void Start () {
        player = GetComponentInParent<LaserBehaviour>();
        if (!GameManager.instance.debugMode)
        {

            //scopeImage.SetActive(false);
            this.enabled = false;
        }
       
        cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {

        scopeImage.transform.position += new Vector3(player.playerInput.GetAxis("Horizontal"), player.playerInput.GetAxis("Vertical"),0)*player.player.cursorSpeed;
        scopeImage.transform.position = new Vector3(
            Mathf.Clamp(scopeImage.transform.position.x,0,Screen.width),
            Mathf.Clamp(scopeImage.transform.position.y,0,Screen.height),
            0
        );
        Vector3 vector = cam.ScreenPointToRay(scopeImage.transform.position).direction;
        //scopeImage.transform.position = cam.transform.position + vector * 50;
        Vector3 targetDir = scopeImage.transform.position - transform.position;
        float step = rotationSpeed * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, vector, step, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
        //RotateObject();
    }

    void RotateObject()
    {
        //Get mouse position
        Vector3 mousePos = Input.mousePosition;

        //Adjust mouse z position
        mousePos.z = cam.transform.position.y - transform.position.y;

        //Get a world position for the mouse
        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(mousePos);

        //Get the angle to rotate and rotate
        float angle = -Mathf.Atan2(transform.position.z - mouseWorldPos.z, transform.position.x - mouseWorldPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, angle, 0), rotationSpeed * Time.deltaTime);
    }
}
