using UnityEngine;

public class ParentPlayerMovement : MonoBehaviour
{
    Transform playerTransform;
    MovingPlatform platform;

    // Start is called before the first frame update
    void Start()
    {
        platform = GetComponent<MovingPlatform>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision with " + collision);
        if (collision.gameObject.tag == "Player")
        {
            playerTransform = collision.transform.parent.parent;
            playerTransform.SetParent(transform);
            // collision.transform.parent.parent.SetParent(transform);
            // collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerTransform.SetParent(null);
            // playerTransform = null;
            // collision.transform.parent.parent.SetParent(null);
            // collision.transform.SetParent(null);

            platform.canMove = false;
        }
    }
}
