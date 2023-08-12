using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Theme : MonoBehaviour
{
    public string title;

    public ThemeManager themeManager;
    public Transform groundedObjectParent;
    public Transform walledObjectParent;
    public Transform wallsParent;
    public bool isThemeActive = false;

    private bool isThemechangeActive = false;
    private Vector3 currentForward;
    private Vector3 reverseForward;

    private void Update()
    {
        if (isThemechangeActive)
        {
            GetCurrentForward();
            if (currentForward != reverseForward)
            {
                if (groundedObjectParent != null)
                    RotateObjects(groundedObjectParent, Vector3.right);

                if (walledObjectParent != null)
                    RotateObjects(walledObjectParent, Vector3.up);

                if (wallsParent != null)
                    RotateObjects(wallsParent, Vector3.up);
            }
            else
            {
                isThemeActive = themeManager.IsThemeActive(this);
                Debug.Log(title + "is active? " + isThemeActive);
                handleColliders(isThemeActive);
                isThemechangeActive = false;
            }
        }
    }

    public void OnThemeChange()
    {
        GetCurrentForward();
        reverseForward = -currentForward;

        handleColliders(false);
        isThemechangeActive = true;
    }

    private void GetCurrentForward()
    {
        if (groundedObjectParent != null)
        {
            currentForward = groundedObjectParent.GetChild(0).forward;
        }
        else if (walledObjectParent != null)
        {
            currentForward = walledObjectParent.GetChild(0).forward;
        }
        else
        {
            currentForward = wallsParent.GetChild(0).forward;
        }

    }
    private void handleColliders(bool _activate)
    {
        if (groundedObjectParent != null)
            ActivateColliders(groundedObjectParent, _activate);

        if (walledObjectParent != null)
            ActivateColliders(walledObjectParent, _activate);

        if (wallsParent != null)
            ActivateColliders(wallsParent, _activate);
    }
    private void RotateObjects(Transform _parentTransform, Vector3 _direction)
    {
        foreach (Transform transform in _parentTransform)
        {
            transform.Rotate(_direction, Space.World);
        }
    }

    private void ActivateColliders(Transform _parentTransform, bool _activate)
    {
        foreach (Transform transform in _parentTransform)
        {
            Collider[] colliders = transform.GetComponentsInChildren<Collider>();
            foreach (Collider collider in colliders)
            {
                collider.enabled = _activate;
            }
        }
    }
}
