using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLayerForStencil : MonoBehaviour
{
    public void SetLayerOfAllChildren(LayerMask _layer)
    {
        gameObject.layer = _layer;

        if (transform.childCount > 0)
        {
            Transform[] children = transform.GetComponentsInChildren<Transform>(includeInactive: true);
            foreach (Transform child in children)
            {
                child.gameObject.layer = _layer;
            }
        }
    }
}
