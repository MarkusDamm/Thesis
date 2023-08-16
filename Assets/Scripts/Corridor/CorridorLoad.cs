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
                ActivateChildObjects(loadObjects, true);
            }
            if (disableObjects != null)
            {
                ActivateChildObjects(disableObjects, false);
            }
            onEnter.Invoke();
        }
    }

    public void ActivateChildObjects(GameObject[] _gameObjects, bool _activate)
    {
        foreach (GameObject objectToLoad in _gameObjects)
        {
            objectToLoad.SetActive(_activate);
        }
    }
}
