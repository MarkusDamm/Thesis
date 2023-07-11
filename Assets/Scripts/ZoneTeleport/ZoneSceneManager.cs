using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ZoneSceneManager : SceneManager
{
    [SerializeField] private InputActionProperty activateValueInteractionLeft;
    [SerializeField] private InputActionProperty activateValueInteractionRight;

    [SerializeField] Zone StartZone;
    [SerializeField] Vector3 PositionOffset;
    [SerializeField] GameObject TeleportationTarget;
    Zone CurrentZone;
    Zone TargetedZone;
    bool canTeleport;
    static float teleportationCooldown = 3f;

    private void Awake()
    {
        teleport(StartZone);
        CurrentZone = StartZone;
        canTeleport = true;
        TeleportationTarget.SetActive(false);
    }

    private void Update()
    {
        float triggerLeft = activateValueInteractionLeft.action.ReadValue<float>();
        float triggerRight = activateValueInteractionRight.action.ReadValue<float>();

        if ((triggerLeft > 0.1f || triggerRight > 0.1f) && canTeleport)
        {
            Debug.Log("triggerLeft: " + triggerLeft + "; triggerRight: " + triggerRight);
            prepareTeleport();

            if (triggerLeft > 0.99f || triggerRight > 0.99f)
            {
                teleport(TargetedZone);
            }
        }
        else
        {
            TeleportationTarget.SetActive(false);
        }

    }

    private void prepareTeleport()
    {
        calculateViewingDirection();
        displayTeleportTarget();
    }

    private void teleport(Zone _zone)
    {
        // playerOrigin.transform.position = _zone.zoneTransf.position + PositionOffset;
        Vector3 playerPos = playerOrigin.transform.position;
        Vector3 ZonePos = _zone.transform.position;
        playerOrigin.transform.Translate(-playerPos + ZonePos + PositionOffset);
        canTeleport = false;
        CurrentZone = _zone;
        Invoke("enableTeleport", ZoneSceneManager.teleportationCooldown);
    }

    private void enableTeleport()
    {
        canTeleport = true;
    }

    private void calculateViewingDirection()
    {
        float yRotation = mainCamera.transform.eulerAngles.y;
        TargetedZone = null;

        if (yRotation < 45 && (yRotation > -45 || yRotation > 315))
        { TargetedZone = CurrentZone.North; }
        else if (yRotation < 135 && yRotation >= 45)
        { TargetedZone = CurrentZone.East; }
        else if ((yRotation < 225 || yRotation < -135) && yRotation >= 135)
        { TargetedZone = CurrentZone.South; }
        else if ((yRotation < -45 || yRotation < 315) && (yRotation >= 225 || yRotation >= -135))
        { TargetedZone = CurrentZone.West; }
    }

    private void displayTeleportTarget()
    {
        if (TargetedZone == null)
        {
            return;
        }
        TeleportationTarget.SetActive(true);
        TeleportationTarget.transform.position = TargetedZone.zoneTransf.position + PositionOffset;
    }
}