using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ZoneTargetProperties
{
    public Zone targetZone;
    public AudioClip audioClip;
    public float distance;
    public float minYRotation;
    public float maxYRotation;
    public Vector3 targetVector;

    [SerializeField, Range(20f, 60f)]
    public float angleSpan = 30f;

    public ZoneTargetProperties(Zone _zone)
    {
        targetZone = _zone;
    }
    public ZoneTargetProperties(Zone _zone, AudioClip _audioClip)
    {
        targetZone = _zone;
        audioClip = _audioClip;
    }

    public void CalculateYRotations(Transform _zoneOfDeparture)
    {
        // Debug.Log("Calculating " + targetZone + " From " + _zoneOfDeparture);
        Vector3 depaturePos = new(_zoneOfDeparture.position.x, 0, _zoneOfDeparture.position.z);
        Vector3 targetPos = new(targetZone.transform.position.x, 0, targetZone.transform.position.z);
        // Vector3 vector3 = _zoneOfDeparture.position - targetZone.transform.position;
        // targetVector = targetPos - depaturePos;
        targetVector = targetZone.transform.position - _zoneOfDeparture.position;
        distance = targetVector.magnitude;
        // vector3.Scale(new Vector3(1, 0, 1));
        // float angle = Vector3.Angle(Vector3.forward, targetVector);
        // // float angle = Vector3.Angle(_zoneOfDeparture.forward, vector3);
        // // angle -= _zoneOfDeparture.localEulerAngles.y;
        // // Debug.Log(angle);
        // while (angle < 0f)
        // {
        //     angle += 360f;
        // }
        // while (angle > 360f)
        // {
        //     angle -= 360f;
        // }
        // minYRotation = angle - angleSpan;
        // maxYRotation = angle + angleSpan;
    }
}

public class Zone : MonoBehaviour
{
    public bool hasOnEnter;
    public UnityEvent onEnter;
    public bool hasOnExit;
    public UnityEvent onExit;
    public AudioSource audioSource;

    [SerializeField] public ZoneTargetProperties[] connectingZones;

    private void Start()
    {
        CalculateConnectedZonesRotations();
        if (!audioSource)
        {
            audioSource = GetComponentInChildren<AudioSource>();
        }
        if (!audioSource)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void CalculateConnectedZonesRotations()
    {
        foreach (ZoneTargetProperties zoneProperties in connectingZones)
        {
            zoneProperties.CalculateYRotations(transform);
        }
    }

}
