using UnityEngine;

public class TriggerPlatformCollider : MonoBehaviour
{
    MovingPlatform platform;
    [SerializeField] GameObject bars;

    // Start is called before the first frame update
    void Start()
    {
        platform = GetComponent<MovingPlatform>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("triggered by " + collider);
        platform.canMove = true;
        bars.SetActive(true);
    }
}
