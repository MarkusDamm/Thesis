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
    Quaternion baseRotation;
    [SerializeField] GameObject TeleportationTarget;
    Zone CurrentZone;
    Zone TargetedZone;
    bool canTeleport;
    static float teleportationCooldown = 3f;

    private void Awake()
    {
        baseRotation = playerOrigin.transform.localRotation;
        CurrentZone = StartZone;
        teleport(StartZone);
        canTeleport = true;
        TeleportationTarget.SetActive(false);
    }

    private void Update()
    {
        float triggerLeft = activateValueInteractionLeft.action.ReadValue<float>();
        float triggerRight = activateValueInteractionRight.action.ReadValue<float>();

        if ((triggerLeft > 0.1f || triggerRight > 0.1f) && canTeleport)
        {
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
        playerOrigin.transform.SetParent(_zone.transform);
        playerOrigin.transform.SetLocalPositionAndRotation(PositionOffset, baseRotation);
        if (CurrentZone.hasOnExit)
        {
            CurrentZone.onExit.Invoke();
        }
        CurrentZone = _zone;
        if (CurrentZone.hasOnEnter)
        {
            CurrentZone.onEnter.Invoke();
        }
        canTeleport = false;
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
        TeleportationTarget.transform.position = TargetedZone.transform.position + PositionOffset + mainCamera.transform.localPosition;
    }
}