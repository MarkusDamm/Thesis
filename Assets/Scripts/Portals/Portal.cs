using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : SceneManager
{
    public Transform avatar;
    public Zone portalTarget;
    [SerializeField] Vector3 PositionOffset = new(-0.75f, 0, 0.05f);

    private Quaternion baseRotation;
    private bool avatarIsOverlapping = false;

    private void Awake()
    {
        baseRotation = playerOrigin.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (avatarIsOverlapping)
        {
            Vector3 portalToAvatar = avatar.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToAvatar);

            Debug.Log(dotProduct);
            if (dotProduct < 0f)
            {
                teleport(portalTarget);
            }
        }
    }

    private void teleport(Zone _zone)
    {
        playerOrigin.transform.SetParent(_zone.transform);
        playerOrigin.transform.SetLocalPositionAndRotation(PositionOffset, baseRotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Avatar Enter by Portal");
            avatarIsOverlapping = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            avatarIsOverlapping = false;
        }
    }
}
