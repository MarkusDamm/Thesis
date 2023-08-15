using UnityEngine;

public class PortalSceneManager : SceneManager
{
    public Transform avatar;

    [SerializeField] Vector3 PositionOffset = new(-0.75f, 0, 0.05f);

    private Quaternion baseRotation;

    protected override void Awake()
    {
        baseRotation = playerOrigin.transform.localRotation;
    }

    public void Teleport(Zone _zone)
    {
        playerOrigin.transform.SetParent(_zone.transform);
        playerOrigin.transform.SetLocalPositionAndRotation(PositionOffset, baseRotation);
    }

}
