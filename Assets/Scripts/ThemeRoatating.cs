using System.Collections;
using Unity.XR.CoreUtils.Datums;
using UnityEngine;

public class ThemeRotating : Theme
{
    public bool isHandlingGroundRotation { get; private set; } = false;

    private void Start()
    {
        themeChangeMethod = ThemeChangeMethod.Rotation;
        isThemeActive = themeManager.IsThemeActive(this);
        ActivateObjects(isThemeActive);
    }
    public override void OnThemeChange()
    {
        base.OnThemeChange();
        StartCoroutine(Rotation());
    }
    public void handleGroundRotation()
    {
        isHandlingGroundRotation = true;
    }
    // public IEnumerator rotateFloor(Transform _rotatingFloorParent)
    // {
    //     int counter = 0;
    //     float speed = 3f / themeManager.themeChangeDuration;
    //     while (counter < 60 * themeManager.themeChangeDuration)
    //     {
    //         counter++;
    //         yield return null;
    //     }
    // }

    IEnumerator Rotation()
    {
        int counter = 0;
        float speed = 3f / themeManager.themeChangeDuration;
        while (counter < 60 * themeManager.themeChangeDuration)
        {
            if (groundedObjectParent != null)
                RotateObjects(groundedObjectParent, Vector3.right, speed);

            if (walledObjectParent != null)
                RotateObjects(walledObjectParent, Vector3.up, speed);

            if (wallsParent != null)
                RotateObjects(wallsParent, Vector3.up, speed);

            if (isHandlingGroundRotation)
                RotateObjects(themeManager.rotatingFloorParent, Vector3.right, speed);

            counter++;
            yield return null;
        }

        isHandlingGroundRotation = false;
        isThemeActive = themeManager.IsThemeActive(this);
        // Debug.Log(title + " is active? " + isThemeActive);
        HandleColliders(isThemeActive);
        ActivateObjects(isThemeActive);
    }
    void RotateObjects(Transform _parentTransform, Vector3 _axis, float _speed)
    {
        foreach (Transform transform in _parentTransform)
        {
            transform.Rotate(_axis, _speed, Space.World);
        }
    }

}
