using UnityEngine;
using UnityEngine.Events;

public class CorridorLoad : MonoBehaviour
{
    public GameObject[] loadObjects;
    public GameObject[] disableObjects;

    public UnityEvent onEnter;

    public void SetLayerOfAllChildren(GameObject _gameObject, LayerMask _layer)
    {
        _gameObject.layer = _layer;

        if (transform.childCount > 0)
        {
            Transform[] children = transform.GetComponentsInChildren<Transform>(includeInactive: true);
            foreach (Transform child in children)
            {
                child.gameObject.layer = _layer;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (loadObjects != null)
            {
                foreach (GameObject objectToLoad in loadObjects)
                {
                    objectToLoad.SetActive(true);
                }
            }
            if (disableObjects != null)
            {
                foreach (GameObject objectToDisable in disableObjects)
                {
                    objectToDisable.SetActive(false);
                }
            }
        }
    }
}
