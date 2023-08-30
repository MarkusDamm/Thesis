using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public Vector3 direction;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
