using UnityEngine;

public enum ThemeChangeMethod
{
    Rotation, Translation
}
public abstract class Theme : MonoBehaviour
{
    public string title;
    public ThemeChangeMethod themeChangeMethod;

    public ThemeManager themeManager;
    public Transform groundedObjectParent;
    public Transform walledObjectParent;
    public Transform wallsParent;
    public bool isThemeActive = false;

    public virtual void OnThemeChange()
    {
        ActivateObjects(true);
        HandleColliders(false);
    }

    protected void HandleColliders(bool _activate)
    {
        if (groundedObjectParent != null)
            ActivateColliders(groundedObjectParent, _activate);

        if (walledObjectParent != null)
            ActivateColliders(walledObjectParent, _activate);

        if (wallsParent != null)
            ActivateColliders(wallsParent, _activate);
    }
    protected void ActivateColliders(Transform _parentTransform, bool _activate)
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
    protected void ActivateObjects(bool _activate)
    {
        if (groundedObjectParent != null)
            groundedObjectParent.gameObject.SetActive(_activate);

        if (walledObjectParent != null)
            walledObjectParent.gameObject.SetActive(_activate);

        if (wallsParent != null)
            wallsParent.gameObject.SetActive(_activate);
    }

}
