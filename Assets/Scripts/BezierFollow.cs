using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierFollow : MonoBehaviour
{
    [SerializeField] private Transform[] routes;
    [SerializeField] private float speedModifier = 2f;
    [SerializeField] private int[] routBreaks;
    int routeToGo;
    float tParam;
    Vector3 targetPosition;
    bool coroutineAllowed;

    // Start is called before the first frame update
    void Start()
    {
        routeToGo = 0;
        tParam = 0f;
        coroutineAllowed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(GoByTheRoute(routeToGo));
        }
    }

    IEnumerator GoByTheRoute(int routeNumber)
    {
        coroutineAllowed = false;

        Vector3 p0 = routes[routeNumber].GetChild(0).position;
        Vector3 p1 = routes[routeNumber].GetChild(1).position;
        Vector3 p2 = routes[routeNumber].GetChild(2).position;
        Vector3 p3 = routes[routeNumber].GetChild(3).position;

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
        }
        else
            coroutineAllowed = true;
    }

    public void startBezierFollow()
    {
        coroutineAllowed = true;
        // StartCoroutine(GoByTheRoute(routeToGo));
    }
}