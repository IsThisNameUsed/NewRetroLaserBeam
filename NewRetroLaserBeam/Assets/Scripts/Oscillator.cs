using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Makes objects float up & down while gently spinning.
public class Oscillator : MonoBehaviour
{
    // User Inputs
    [Range(0, 50)] public float degreesPerSecond = 15.0f;
    [Range(0, 4)] public float amplitude = 0.5f;
    [Range(0, 4)] public float frequency = 0.8f;

    // Position Storage Variables
    float posOffset;
    Vector3 tempPos = new Vector3();
    public GameObject pGameObject;
    // Use this for initialization
    void Start()
    {
        // Store the starting position of the object
        posOffset = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Float up/down with a Sin()
        tempPos = new Vector3(transform.position.x, posOffset, transform.position.z);
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
        tempPos.y = Mathf.Lerp(transform.position.y, tempPos.y, 0.5f);
        transform.position = new Vector3(transform.position.x, tempPos.y, transform.position.z);
    }
}