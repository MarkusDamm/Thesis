using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    // [SerializeField] private InputActionProperty pinchAnimationAction;
    // [SerializeField] private InputActionProperty gripAnimationAction;
    [SerializeField] private bool isRight;
    [SerializeField] private Animator handAnimator;
    private string hand;

    // Start is called before the first frame update
    void Start()
    {
        handAnimator.SetFloat("Trigger", 0);
        handAnimator.SetFloat("Grip", 0);

        if (isRight)
        {
            hand = "Right";
        }
        else
        {
            hand = "Left";
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("Left Trigger: " + Input.GetAxis("XRI_Left_Trigger") + " - Left Grip: " + Input.GetAxis("XRI_Left_Grip"));
        // Debug.Log("Left Primary2DAxis_Vertical: " + Input.GetAxis("XRI_Left_Primary2DAxis_Vertical"));
        // Debug.Log("Left Primary2DAxis_Horizontal: " + Input.GetAxis("XRI_Left_Primary2DAxis_Horizontal"));
        // Debug.Log("Left XRI_Left_Primary2DAxisClick: " + Input.GetAxis("XRI_Left_Primary2DAxisClick"));

        // Debug.Log("Left XRI_Left_IndexTouch: " + Input.GetAxis("XRI_Left_IndexTouch"));  // no use
        // Debug.Log("Left XRI_Left_ThumbTouch: " + Input.GetAxis("XRI_Left_ThumbTouch"));  // no use
        // Debug.Log("Left XRI_Left_PrimaryButton: " + Input.GetAxis("XRI_Left_PrimaryButton"));  // no use
        // Debug.Log("Left XRI_Left_SecondaryButton: " + Input.GetAxis("XRI_Left_SecondaryButton"));  // no use
        // Debug.Log("Left XRI_Left_PrimaryTouch: " + Input.GetAxis("XRI_Left_PrimaryTouch"));  // no use
        // Debug.Log("Left XRI_Left_SecondaryTouch: " + Input.GetAxis("XRI_Left_SecondaryTouch"));  // no use
        // Debug.Log("Left XRI_Left_Thumbrest: " + Input.GetAxis("XRI_Left_Thumbrest"));  // no use

        // float triggerValue = pinchAnimationAction.action.ReadValue<float>();
        float triggerValue = Input.GetAxis("XRI_" + hand + "_Trigger");
        handAnimator.SetFloat("Trigger", triggerValue);

        // float gripValue = gripAnimationAction.action.ReadValue<float>();
        float gripValue = Input.GetAxis("XRI_" + hand + "_Grip");
        handAnimator.SetFloat("Grip", gripValue);

        // Debug.Log("TriggerV: " + triggerValue + "  GripV: " + gripValue);
    }
}
