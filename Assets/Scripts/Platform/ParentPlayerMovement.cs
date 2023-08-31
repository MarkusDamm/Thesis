using UnityEngine;

public class ParentPlayerMovement : MonoBehaviour
{
    Transform playerTransform;
    MovingPlatform platform;
    Transform previousParent;


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
        if(playerTransform == null)
            playerTransform = GameObject.Find("SzeneManager").GetComponent<SceneManager>().playerOrigin.transform;

        if (isTrue)
        {
            previousParent = playerTransform.parent;
            playerTransform.SetParent(transform);
        }

        else
        {
            playerTransform.SetParent(previousParent);
            platform.canMove = false;
            previousParent = null;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && platform.canMove == false )
        {
            ParentPlayer(false);
        }
    }
}
