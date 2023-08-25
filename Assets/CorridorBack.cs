using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorBack : MonoBehaviour
{

    public CorridorLoad corridorLoad;

    private void Start()
    {
        if (!corridorLoad)
        {
            corridorLoad = transform.parent.GetComponentInChildren<CorridorLoad>();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (corridorLoad.loadObjects != null)
            {
                corridorLoad.ActivateChildObjects(corridorLoad.loadObjects, false);
            }
            if (corridorLoad.disableObjects != null)
            {
                corridorLoad.ActivateChildObjects(corridorLoad.disableObjects, true);
            }
            // corridorLoad.onEnter.Invoke(); Aber mit gegen-event...
        }

    }
}
