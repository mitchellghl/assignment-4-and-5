using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public Transform centerEye;
    float positionZ;
    public float speed = 0.1f;
    public float deadZoneThresh = 0.05f;
    public Transform controllerLocation;
    public RaycastHit teleportationHit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).magnitude > deadZoneThresh)
        {
            transform.position += centerEye.forward * speed * Time.deltaTime;
        }

        Physics.Raycast(controllerLocation.transform.position, controllerLocation.transform.forward, out teleportationHit);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = teleportationHit.point;
        }

    }
}
