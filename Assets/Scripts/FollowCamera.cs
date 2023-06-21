using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = cameraTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraTransform.position;
    }
}
