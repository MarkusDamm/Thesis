using UnityEngine;

public class Portal : MonoBehaviour
{
    public PortalSceneManager portalSceneManager;
    public Zone portalTarget;
    private bool avatarIsOverlapping = false;

    private void Start()
    {
        if (!portalSceneManager)
        {
            portalSceneManager = GameObject.Find("SzeneManager").GetComponent<PortalSceneManager>();
        }
        if (!portalSceneManager)
        {
            portalSceneManager = GameObject.Find("SceneManager").GetComponent<PortalSceneManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (avatarIsOverlapping)
        {
            Vector3 portalToAvatar = portalSceneManager.avatar.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToAvatar);

            if (dotProduct < 0f)
            {
                portalSceneManager.Teleport(portalTarget);
            }
        }
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
