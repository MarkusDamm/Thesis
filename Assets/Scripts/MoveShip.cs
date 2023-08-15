using UnityEngine;
using UnityEngine.Events;

public class MoveShip : MonoBehaviour
{
    [SerializeField] BezierFollow followScript;
    [SerializeField] UnityEvent[] startCrouseEvents;
    [SerializeField] UnityEvent[] endCrouseEvents;
    bool isShipMoving = false;

    private void Start()
    {
        if (!followScript)
            followScript = gameObject.GetComponent<BezierFollow>();
    }

    public void StartCrouse()
    {
        // Debug.Log("Start Crouse");
        if (!isShipMoving)
        {
            followScript.StartBezierFollow(this);
        }
    }

    public void EndCrouse()
    {
        // Debug.Log("End Crouse");
    }

    public void HndCrouse(bool _isOver, int _currentPoint)
    {
        string msg = _isOver ? "Crouse ended at " : "Crouse starts at ";
        msg += _currentPoint + ". point. ";
        Debug.Log(msg);

        if (_isOver)
        {
            isShipMoving = false;
            endCrouseEvents[_currentPoint].Invoke();
        }
        else
        {
            isShipMoving = true;
            startCrouseEvents[_currentPoint].Invoke();
        }
    }
}