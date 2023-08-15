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

    public void CalculateTargetVectos(Transform _zoneOfDeparture)
    {
        targetVector = targetZone.transform.position - _zoneOfDeparture.position;
        distance = targetVector.magnitude;
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
            zoneProperties.CalculateTargetVectos(transform);
        }
    }

}
