using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovingZone : Zone
{
    private bool isZoneMoving = false;

    public void ZoneMoves(bool _isMoving)
    {
        isZoneMoving = _isMoving;
        StartCoroutine(HandeZoneMove());
    }

    IEnumerator HandeZoneMove()
    {
        while (isZoneMoving)
        {
            foreach (ZoneProperties zoneProperties in connectingZones)
            {
                zoneProperties.CalculateYRotations(transform);
            }
            yield return new WaitForSeconds(1);
        }
    }
}
