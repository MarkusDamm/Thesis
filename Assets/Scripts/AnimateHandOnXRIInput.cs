using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnXRIInput : MonoBehaviour
{
    [Tooltip("XRI Hand Interaction / Hand Activate Value")]
    [SerializeField] private InputActionProperty activateValueInteraction;
    [SerializeField] private InputActionProperty grabMoveLocomotion;
    [SerializeField] private Animator handAnimator;
    private string hand;

    // Start is called before the first frame update
    void Start()
    {
        handAnimator.SetFloat("Trigger", 0);
        handAnimator.SetFloat("Grip", 0);

    }

    // Update is called once per frame
    void Update()
    {
        float triggerValue = activateValueInteraction.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggerValue);

        float gripValue = grabMoveLocomotion.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripValue);
    }
}
