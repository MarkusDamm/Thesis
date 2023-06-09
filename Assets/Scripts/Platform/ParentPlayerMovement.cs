using UnityEngine;

public class ParentPlayerMovement : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision with " + collision);
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.parent.parent.SetParent(transform);
            // collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.parent.parent.SetParent(null);
            // collision.transform.SetParent(null);
        }
    }
}
