using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;
// using UnityEngine.UIElements;

public class ZoneSceneManager : SceneManager
{
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private InputActionProperty activateValueInteractionLeft;
    [SerializeField] private InputActionProperty activateValueInteractionRight;

    [SerializeField] Canvas fadingCanvas;

    [SerializeField] Zone StartZone;
    [SerializeField] Vector3 PositionOffset;
    Quaternion baseRotation;
    [SerializeField] GameObject TeleportationTarget;
    Zone CurrentZone;
    float viewingDirection;
    ZoneProperties TargetedZonesProperty;
    bool canTeleport;
    [SerializeField][Range(0.5f, 3f)] float teleportFadeDuration = 1f;
    static float teleportationCooldown = 3f;

    private void Awake()
    {
        baseRotation = playerOrigin.transform.localRotation;
        CurrentZone = StartZone;
        teleport(StartZone);
        canTeleport = true;
        TeleportationTarget.SetActive(false);

        if (!fadingCanvas)
        {
            fadingCanvas = transform.GetComponentInChildren<Canvas>();
        }
        if (!audioManager)
        {
            audioManager = transform.GetComponent<AudioManager>();
        }


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
                // audioManager.PlaySoundOnSource(TargetedZonesProperty.audioClip, CurrentZone.audioSource);
                CurrentZone.audioSource.PlayOneShot(TargetedZonesProperty.audioClip);
                Debug.Log("Teleport Player");
                canTeleport = false;
                Invoke("enableTeleport", ZoneSceneManager.teleportationCooldown);
                StartCoroutine(Teleport());
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

    private IEnumerator Teleport()
    {
        // Play sound
        // CurrentZone.audioSource.PlayOneShot(TargetedZonesProperty.audioClip);
        // Debug.Log("Play Sound from " + _zoneProperties.targetZone);

        // Fade to Black
        UnityEngine.UI.Image fadingImage = fadingCanvas.GetComponentInChildren<UnityEngine.UI.Image>();
        float faderSequenceDuration = teleportFadeDuration / 3f;
        float fadingValuePerFrame = 1f / (60 * faderSequenceDuration);
        float fadingValue = 0f;
        Debug.Log("FaderSequenceDuration is " + faderSequenceDuration);
        for (int i = 0; i < (faderSequenceDuration * 60f); i++)
        {
            fadingValue += fadingValuePerFrame;
            SetFadeColorAlpha(fadingImage, fadingValue);
            yield return new WaitForFixedUpdate();
        }
        SetFadeColorAlpha(fadingImage, 1);
        Debug.Log("FadeColor is Black");

        // Teleport
        playerOrigin.transform.SetParent(TargetedZonesProperty.targetZone.transform);
        playerOrigin.transform.SetLocalPositionAndRotation(PositionOffset, baseRotation);
        if (CurrentZone.hasOnExit)
        {
            CurrentZone.onExit.Invoke();
        }
        CurrentZone = TargetedZonesProperty.targetZone;
        if (CurrentZone.hasOnEnter)
        {
            CurrentZone.onEnter.Invoke();
        }

        Debug.Log("Wait for Seconds");
        yield return new WaitForSeconds(faderSequenceDuration);
        Debug.Log("Wait over");
        // Fade Back
        for (int j = 0; j < faderSequenceDuration * 60f; j++)
        {
            fadingValue -= fadingValuePerFrame;
            SetFadeColorAlpha(fadingImage, fadingValue);
            yield return new WaitForFixedUpdate();
        }
        SetFadeColorAlpha(fadingImage, 0);
    }

    private void SetFadeColorAlpha(UnityEngine.UI.Image _image, float _alphaValue)
    {
        Color fadeColor = _image.color;
        fadeColor.a = _alphaValue;
        _image.color = fadeColor;
    }

    private void enableTeleport()
    {
        canTeleport = true;
    }

    private void calculateViewingDirection()
    {
        viewingDirection = mainCamera.transform.eulerAngles.y;

        TargetedZonesProperty = null;
        foreach (ZoneProperties zoneProperties in CurrentZone.connectingZones)
        {
            if (viewingDirection < zoneProperties.maxYRotation && viewingDirection > zoneProperties.minYRotation)
            {
                TargetedZonesProperty = zoneProperties;
                // Debug.Log("Targetet Zone is " + TargetedZonesProperty.targetZone);
                return;
            }
        }

    }

    private void displayTeleportTarget()
    {
        if (TargetedZonesProperty == null)
        {
            return;
        }
        TeleportationTarget.SetActive(true);
        TeleportationTarget.transform.position = TargetedZonesProperty.targetZone.transform.position + mainCamera.transform.localPosition + Vector3.down;
        //+ PositionOffset
    }
}