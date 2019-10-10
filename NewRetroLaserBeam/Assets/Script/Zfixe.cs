using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zfixe : MonoBehaviour
{
    public float zStart;
    public Quaternion startRotation;
    // Start is called before the first frame update
    void Start()
    {
        zStart = transform.position.z;
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, zStart);
        transform.position = newPosition;
        transform.rotation = startRotation;
    }
}
