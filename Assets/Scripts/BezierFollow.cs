using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierFollow : MonoBehaviour
{
    [SerializeField] private Transform[] routes;
    [SerializeField] private float speedModifier = 2f;
    [SerializeField] private int[] routeBreaks;
    int routeBreakCounter;
    int routeToGo;
    float tParam;
    Vector3 targetPosition;
    public bool coroutineAllowed { get; private set; }
    public bool coroutineRunning { get; private set; }

    MoveShip shipScript;

    // Start is called before the first frame update
    void Start()
    {
        routeBreakCounter = 0;
        routeToGo = 0;
        tParam = 0f;
        coroutineAllowed = false;
        coroutineRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(GoByTheRoute(routeToGo));
        }
    }

    IEnumerator GoByTheRoute(int _routeNumber)
    {
        coroutineAllowed = false;
        coroutineRunning = true;

        Vector3 p0 = routes[_routeNumber].GetChild(0).position;
        Vector3 p1 = routes[_routeNumber].GetChild(1).position;
        Vector3 p2 = routes[_routeNumber].GetChild(2).position;
        Vector3 p3 = routes[_routeNumber].GetChild(3).position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;

            targetPosition = Mathf.Pow(1 - tParam, 3) * p0 +
                3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                Mathf.Pow(tParam, 3) * p3;

            transform.LookAt(targetPosition);
            transform.position = targetPosition;
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;
        routeToGo++;

        if (routeToGo > routes.Length - 1)
        {
            routeToGo = 0;
            coroutineAllowed = false;
            routeBreakCounter = 0;
            shipScript.HndCrouse(true, routeBreakCounter);
        }
        else if (routeBreakCounter < routeBreaks.Length)
        {
            if (routeToGo == routeBreaks[routeBreakCounter])
            {
                coroutineAllowed = false;
                routeBreakCounter++;
                shipScript.HndCrouse(true, routeBreakCounter);
            }
            else
                coroutineAllowed = true;
        }
        else
            coroutineAllowed = true;

        coroutineRunning = false;
    }

    public void StartBezierFollow(MoveShip _shipScript)
    {
        shipScript = _shipScript;
        shipScript.HndCrouse(false, routeBreakCounter);
        coroutineAllowed = true;
    }
}