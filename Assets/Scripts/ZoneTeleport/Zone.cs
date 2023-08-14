using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ZoneProperties
{
    public Zone targetZone;
    public AudioClip audioClip;
    public float distance;
    public float minYRotation;
    public float maxYRotation;

    [SerializeField, Range(20f, 60f)]
    public float angleSpan = 30f;

    public void CalculateYRotations(Transform _zoneOfDeparture)
    {
        // Debug.Log("Calculating " + targetZone + " From " + _zoneOfDeparture);
        Vector3 vector3 = _zoneOfDeparture.position - targetZone.transform.position;
        distance = vector3.magnitude;
        vector3.Scale(new Vector3(1, 0, 1));
        float angle = Vector3.Angle(_zoneOfDeparture.forward, vector3);
        angle -= _zoneOfDeparture.eulerAngles.y;
        // Debug.Log(angle);
        while (angle < 0f)
        {
            angle += 360f;
        }
        while (angle > 360f)
        {
            angle -= 360f;
        }
        minYRotation = angle - angleSpan;
        maxYRotation = angle + angleSpan;
    }
}

public class Zone : MonoBehaviour
{
    public bool hasOnEnter;
    public UnityEvent onEnter;
    public bool hasOnExit;
    public UnityEvent onExit;
    public AudioSource audioSource;

    [SerializeField] public ZoneProperties[] connectingZones;

    private void Start()
    {
        foreach (ZoneProperties zoneProperties in connectingZones)
        {
            zoneProperties.CalculateYRotations(transform);
        }

        if (!audioSource)
        {
            audioSource = GetComponentInChildren<AudioSource>();
        }
        if (!audioSource)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

}
