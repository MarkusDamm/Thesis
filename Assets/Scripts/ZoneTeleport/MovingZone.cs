using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovingZone : Zone
{
    private bool isZoneMoving = false;

    public void ZoneConnectionChange(Zone _zone, bool _disconnectedOrNot)
    {
        Debug.Log(_zone + " has been disconnected from " + this + "?  " + _disconnectedOrNot);

        CalculateConnectedZonesRotations();
    }

    // public void ConnectZone(ZoneProperties _zone)
    // {
    //     connectingZones.SetValue(_zone, connectingZones.Length);
    //     CalculateConnectedZonesRotations();
    // }
    public void ConnectZone(Zone _zone)
    {
        ZoneTargetProperties connectZoneProperties = new(_zone);
        ZoneTargetProperties[] newArray = new ZoneTargetProperties[connectingZones.Length + 1];
        int counter = 0;
        foreach (ZoneTargetProperties zoneProperties in connectingZones)
        {
            newArray[counter] = zoneProperties;
            counter++;
        }
        newArray[counter] = connectZoneProperties;
        connectingZones = new ZoneTargetProperties[connectingZones.Length];
        connectingZones = newArray;
        CalculateConnectedZonesRotations();
    }
    // public void ConnectZone(Zone _zone, AudioClip _audioClip)
    // {
    //     ZoneProperties properties = new(_zone, _audioClip);
    //     connectingZones.SetValue(_zone, connectingZones.Length);
    //     CalculateConnectedZonesRotations();
    // }

    public void DisconnectZone(Zone _zone)
    {
        ZoneTargetProperties[] newArray = new ZoneTargetProperties[connectingZones.Length - 1];
        int counter = 0;
        foreach (ZoneTargetProperties zoneProperties in connectingZones)
        {
            if (zoneProperties.targetZone != _zone)
            {
                newArray[counter] = zoneProperties;
                counter++;
            }
        }
        connectingZones = new ZoneTargetProperties[connectingZones.Length];
        connectingZones = newArray;
        CalculateConnectedZonesRotations();
    }
    // public void DisconnectZone(ZoneProperties _zone)
    // {
    //     for (int i = 0; i < connectingZones.Length; i++)
    //     {
    //         if (connectingZones[i] == _zone)
    //         {
    //             connectingZones.SetValue(null, i);
    //         }
    //     }
    //     CalculateConnectedZonesRotations();
    // }

    public void ZoneMoves(bool _isMoving)
    {
        isZoneMoving = _isMoving;
        if (isZoneMoving)
            StartCoroutine(HandeZoneMove());
    }

    IEnumerator HandeZoneMove()
    {
        while (isZoneMoving)
        {
            CalculateConnectedZonesRotations();
            yield return new WaitForSeconds(1);
        }
    }
}
