using UnityEngine;

public class TriggerPlatformCollider : MonoBehaviour
{
    MovingPlatform platform;
    [SerializeField] GameObject bars;

    // Start is called before the first frame update
    void Start()
    {
        platform = GetComponent<MovingPlatform>();
        if (platform.usesInput)
        {
            this.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!platform.usesInput)
        {
            platform.canMove = true;
            bars.SetActive(true);
        }
    }
}
