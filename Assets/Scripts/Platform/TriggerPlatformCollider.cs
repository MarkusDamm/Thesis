using UnityEngine;

public class TriggerPlatformCollider : MonoBehaviour
{
    MovingPlatform platform;

    // Start is called before the first frame update
    void Start()
    {
        platform = GetComponent<MovingPlatform>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("triggered by " + collider);
        platform.canMove = true;
    }
}
