using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ZoneSceneManager : SceneManager
{
    [SerializeField] private InputActionProperty activateValueInteractionLeft;
    [SerializeField] private InputActionProperty activateValueInteractionRight;

    [SerializeField] Zone startZone;
    [SerializeField] Vector3 positionOffset;
    Quaternion baseRotation;
    [SerializeField] GameObject teleportationTarget;
    Zone currentZone;
    float viewingDirection;
    ZoneTargetProperties targetedZonesProperty;
    bool canTeleport;
    [SerializeField][Range(0.5f, 3f)] float teleportFadeDuration = 1f;
    static readonly float teleportationCooldown = 3f;

    protected override void Awake()
    {
        base.Awake();

        baseRotation = playerOrigin.transform.localRotation;
        currentZone = startZone;
        TeleportPlayer(startZone);
        canTeleport = true;
        teleportationTarget.SetActive(false);
    }

    private void Update()
    {
        float triggerLeft = activateValueInteractionLeft.action.ReadValue<float>();
        float triggerRight = activateValueInteractionRight.action.ReadValue<float>();

        if ((triggerLeft > 0.1f || triggerRight > 0.1f) && canTeleport)
        {
            PrepareTeleport();

            if ((triggerLeft > 0.99f || triggerRight > 0.99f) && targetedZonesProperty != null)
            {
                // currentZone.audioSource.PlayOneShot(targetedZonesProperty.audioClip);
                Debug.Log("Teleport Player");
                canTeleport = false;
                Invoke("EnableTeleport", ZoneSceneManager.teleportationCooldown);
                StartCoroutine(HndTeleport());
            }
        }
        else
        {
            teleportationTarget.SetActive(false);
        }

    }

    private void PrepareTeleport()
    {
        CalculateViewingDirection();
        DisplayTeleportTarget();
    }

    private void TeleportPlayer(Zone _zone)
    {
        playerOrigin.transform.SetParent(_zone.transform);
        playerOrigin.transform.SetLocalPositionAndRotation(positionOffset, baseRotation);
        if (currentZone.hasOnExit)
        {
            currentZone.onExit.Invoke();
        }
        currentZone = _zone;
        if (currentZone.hasOnEnter)
        {
            currentZone.onEnter.Invoke();
        }
        canTeleport = false;
        Invoke("EnableTeleport", ZoneSceneManager.teleportationCooldown);
    }

    private IEnumerator HndTeleport()
    {
        // Play sound
        currentZone.audioSource.PlayOneShot(targetedZonesProperty.audioClip);

        // Fade to Black
        float faderSequenceDuration = teleportFadeDuration / 3f;
        yield return StartCoroutine(Fade(true, faderSequenceDuration));

        // Teleport
        TeleportPlayer(targetedZonesProperty.targetZone);
        yield return new WaitForSeconds(faderSequenceDuration);

        // Fade Back
        yield return StartCoroutine(Fade(false, faderSequenceDuration));
    }

    private void EnableTeleport()
    {
        canTeleport = true;
    }

    private void CalculateViewingDirection()
    {
        viewingDirection = mainCamera.transform.eulerAngles.y;
        Debug.Log("Viewing Direction " + viewingDirection);
        targetedZonesProperty = null;
        foreach (ZoneTargetProperties zoneProperties in currentZone.connectingZones)
        {
            float viewingAngle = Vector3.Angle(mainCamera.transform.forward, zoneProperties.targetVector.normalized);
            if (viewingAngle < zoneProperties.angleSpan)
            {
                targetedZonesProperty = zoneProperties;
                return;
            }
            // if (viewingDirection < zoneProperties.maxYRotation && viewingDirection > zoneProperties.minYRotation)
            // {
            //     targetedZonesProperty = zoneProperties;
            //     return;
            // }
        }
    }

    private void DisplayTeleportTarget()
    {
        if (targetedZonesProperty == null)
        {
            return;
        }
        teleportationTarget.SetActive(true);
        teleportationTarget.transform.position = targetedZonesProperty.targetZone.transform.position + mainCamera.transform.localPosition + Vector3.down;
        //+ PositionOffset
    }
}