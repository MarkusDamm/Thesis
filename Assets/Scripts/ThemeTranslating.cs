using System.Collections;
using UnityEngine;

public class ThemeTranslating : Theme
{
    public Transform disabledTransform;
    public Transform activeTransform;

    private void Start()
    {
        themeChangeMethod = ThemeChangeMethod.Translation;
        isThemeActive = themeManager.IsThemeActive(this);
        ActivateObjects(isThemeActive);
    }

    public override void OnThemeChange()
    {
        base.OnThemeChange();
        StartCoroutine(Translation());
    }

    IEnumerator Translation()
    {
        int counter = 0;
        Vector3 distance;
        if (isThemeActive)
        {
            distance = disabledTransform.position - activeTransform.position;
        }
        else
        {
            distance = activeTransform.position - disabledTransform.position;
        }
        // Debug.Log(distance);
        Vector3 distancePerSecond = distance / themeManager.themeChangeDuration;
        while (counter < 60 * themeManager.themeChangeDuration)
        {
            if (groundedObjectParent != null)
                TranslateObjects(groundedObjectParent, distancePerSecond / 60);

            if (walledObjectParent != null)
                TranslateObjects(walledObjectParent, distancePerSecond / 60);

            if (wallsParent != null)
                TranslateObjects(wallsParent, distancePerSecond / 60);

            counter++;
            yield return null;
        }

        isThemeActive = themeManager.IsThemeActive(this);
        // Debug.Log(title + " is active? " + isThemeActive);
        HandleColliders(isThemeActive);
        ActivateObjects(isThemeActive);
    }
    void TranslateObjects(Transform _parentTransform, Vector3 _direction)
    {
        foreach (Transform transform in _parentTransform)
        {
            transform.Translate(_direction, Space.World);
        }
    }


}
