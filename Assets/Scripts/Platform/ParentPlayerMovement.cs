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
        Debug.Log(gameObject + " collision with " + collision);
        if (collision.gameObject.tag == "Player" && !platform.usesInput)
        {
            playerTransform = collision.transform.parent;
            ParentPlayer(true);
            // playerTransform.SetParent(transform);

            // collision.transform.parent.parent.SetParent(transform);
            // collision.transform.SetParent(transform);
        }
    }

    public void ParentPlayer(bool isTrue)
    {
        if (isTrue)
            playerTransform.SetParent(transform);

        else
        {
            playerTransform.SetParent(null);
            platform.canMove = false;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player" /* && !platform.usesInput */ )
        {
            ParentPlayer(false);
            // playerTransform.SetParent(null);

            // playerTransform = null;
            // collision.transform.parent.parent.SetParent(null);
            // collision.transform.SetParent(null);

            // platform.canMove = false;
        }
    }
}
