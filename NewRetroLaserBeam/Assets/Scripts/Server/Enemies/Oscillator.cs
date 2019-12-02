using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Makes objects float up & down while gently spinning.
public class Oscillator : MonoBehaviour
{
    // User Inputs
    [Range(-5, 5)] public float amplitudeX = 1f;
    [Range(0, 4)] public float frequencyX = 0.8f;
    [Range(-5, 5)] public float amplitudeY = 0.5f;
    [Range(0, 4)] public float frequencyY = 1.2f;

    // Position Storage Variables
    float posXOffset;
    float posYOffset;
    Vector3 tempPos = new Vector3();
    // Use this for initialization
    void Start()
    {
        // Store the starting position of the object
        posXOffset = transform.localPosition.x;
        posYOffset = transform.localPosition.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Float up/down with a Sin()
        tempPos = new Vector3(posXOffset,posYOffset,transform.localPosition.z);
        tempPos.x += Mathf.Sin(Time.fixedTime * Mathf.PI * frequencyX) * amplitudeX;
        tempPos.x = Mathf.Lerp(transform.localPosition.x, tempPos.x, 0.5f);

        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequencyY) * amplitudeY;
        tempPos.y = Mathf.Lerp(transform.localPosition.y, tempPos.y, 0.5f);
        transform.localPosition = tempPos;
    }
}
