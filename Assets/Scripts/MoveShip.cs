using UnityEngine;

public class MoveShip : MonoBehaviour
{
    [SerializeField] BezierFollow followScript;

    private void Start()
    {
        if (!followScript)
            followScript = gameObject.GetComponent<BezierFollow>();
    }

    public void startCrouse()
    {
        Debug.Log("Start Crouse");
        followScript.startBezierFollow();

    }
    public void endCrouse()
    {
        Debug.Log("End Crouse");
    }
}