using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialsOfChildren : MonoBehaviour
{

    public void OnChange(Material _newMaterial)
    {
        MeshRenderer[] meshRenderersOfChildren = transform.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer item in meshRenderersOfChildren)
        {
            item.material = _newMaterial;
            item.SetMaterials(new List<Material>() { _newMaterial });
        }

    }
}
