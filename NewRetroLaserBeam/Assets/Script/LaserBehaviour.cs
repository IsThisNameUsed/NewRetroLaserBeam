using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{

    public GameObject scopeImage;
    LineRenderer laser;
    public Ray ray;
    public RaycastHit hit;
    public static LaserManager laserManager;
    public bool isShooting;
    [Range(0, 3)] public int playerId;
    void Awake()
    {
        laser = GetComponent<LineRenderer>();
    }
    private void Start()
    {
        SetLaserActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        UpdateLaserRootPosition();
        UpdateLaserPositions();
#if UNITY_EDITOR
        if (playerId == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SetLaserActive(!isShooting);
            }
            Vector3 vector = laserManager.mainCamera.ScreenPointToRay(Input.mousePosition).direction;

            scopeImage.transform.position = laserManager.mainCamera.transform.position + vector.normalized;
        }
#endif
    }

    public void UpdateLaserRootPosition()
    {
        ray = laserManager.mainCamera.ScreenPointToRay(new Vector3((laserManager.mainCamera.pixelWidth / (LaserManager.playingPlayers + 1)) * (playerId + 1), 0, 0));
        laser.SetPosition(0, ray.origin);
        //transform.GetChild(_laserArray).position = ray.origin;
    }
    public void UpdateLaserPositions()
    {
        ray = laserManager.mainCamera.ScreenPointToRay(laserManager.mainCamera.WorldToScreenPoint(scopeImage.transform.position));
        if (Physics.Raycast(ray, out hit))
        {
            laser.SetPosition(1, hit.point);
        }
        else
        {
            laser.SetPosition(1, scopeImage.transform.position);
        }
    }
    public bool SetLaserActive(bool _state)
    {
        isShooting = _state;
        return laser.enabled = _state;
    }
}

