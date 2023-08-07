using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public Transform mainCamera;
    public Transform portal;
    public Transform otherPortal;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Camera>().fieldOfView = mainCamera.GetComponent<Camera>().fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 avatarOffsetFromPortal = mainCamera.position - otherPortal.position;
        transform.position = portal.position + avatarOffsetFromPortal;

        float angularDifferenceBetweenPortalRotations = Quaternion.Angle(portal.rotation, otherPortal.rotation);
        Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
        Vector3 newCameraDirection = portalRotationalDifference * mainCamera.forward;
        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);

    }
}
