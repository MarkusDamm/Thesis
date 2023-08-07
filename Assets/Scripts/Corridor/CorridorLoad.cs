using UnityEngine;

public class CorridorLoad : MonoBehaviour
{
    public GameObject loadRoom;
    public GameObject disableRoom;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (loadRoom != null)
            {
                loadRoom.SetActive(true);
            }
            if (disableRoom != null)
            {
                disableRoom.SetActive(false);
            }
        }
    }
}
