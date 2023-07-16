using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

enum ManagerScale
{
    Tiny, Normal, Giant
}
public class ScaleSceneManager : SceneManager
{
    [SerializeField] float scaleMultiplier = 0.01f;
    [SerializeField] private InputActionProperty grabLeftLocomotion;
    [SerializeField] private InputActionProperty grabRightLocomotion;
    [SerializeField] Transform leftHand;
    [SerializeField] Transform rightHand;
    bool bothPressed = false;
    float controllerDistance;
    ManagerScale currentScale = ManagerScale.Normal;

    // Update is called once per frame
    void Update()
    {
        // Find Input
        InputAction leftGripAction = grabLeftLocomotion.action;
        InputAction rightGripAction = grabRightLocomotion.action;

        if (leftGripAction.IsPressed() && rightGripAction.IsPressed())
        {
            if (leftGripAction.triggered || rightGripAction.triggered)
            {
                bothPressed = true;
                checkDualGrab(true);
            }
        }
        if (bothPressed && (leftGripAction.WasReleasedThisFrame() || rightGripAction.WasReleasedThisFrame()))
        {
            bothPressed = false;
            checkDualGrab(false);
        }

    }

    void checkDualGrab(bool _triggered)
    {
        // Vector3 leftContrPos = playerOrigin.transform.Find("LeftHand").position;
        // Vector3 rightContrPos = playerOrigin.transform.Find("RightHand").position;
        Vector3 leftContrPos = leftHand.position;
        Vector3 rightContrPos = rightHand.position;
        float currentControllerDistance = (leftContrPos - rightContrPos).magnitude;
        Debug.Log(currentControllerDistance);
        if (_triggered)
        {
            controllerDistance = currentControllerDistance;
        }
        else
        {
            if (currentControllerDistance > controllerDistance)
            {
                ScaleAvatar(true);
            }
            else if (currentControllerDistance < controllerDistance)
            {
                ScaleAvatar(false);
            }
        }
    }

    void ScaleAvatar(bool _scaleDown)
    {
        float scaler;
        if (_scaleDown && currentScale != ManagerScale.Tiny)
        {
            scaler = scaleMultiplier;
            currentScale--;
        }
        else if (currentScale != ManagerScale.Giant)
        {
            scaler = 1 / scaleMultiplier;
            currentScale++;
        }
        else return;

        playerOrigin.transform.localScale = playerOrigin.transform.localScale * scaler;
    }

    // void ScaleWorld(bool _scaleDown)
    // {
    //     float scaler;
    //     if (_scaleDown)
    //     { scaler = scaleMultiplier; }
    //     else
    //     { scaler = 1 / scaleMultiplier; }

    //     foreach (Transform scalingObject in scalingObjects)
    //     {
    //         scalingObject.localScale = scalingObject.localScale * scaler;
    //     }
    //     terrainScaler.scale(scaler);
    // }
}