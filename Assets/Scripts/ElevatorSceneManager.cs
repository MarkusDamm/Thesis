using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorSceneManager : SceneManager
{
    public Transform elevator;
    public MovingPlatform platformScript;
    private int currentFloor = 0;
    private int lastFloor = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentFloor = EstimateFloor();
    }

    // Update is called once per frame
    void Update()
    {
        currentFloor = EstimateFloor();
        Debug.Log("Current Floor: " + currentFloor + ".  Last Floor: " + lastFloor);
        if (!platformScript.canMove && platformScript.currentLevel != currentFloor)
        {
            platformScript.MoveToLevel(currentFloor);
            lastFloor = currentFloor;
        }
    }

    int EstimateFloor()
    {
        float currentHeigth = mainCamera.transform.position.y;
        int floorNumber = -1;
        for (int i = 0; i < platformScript.points.Length; i++)
        {
            if (currentHeigth > platformScript.points[i].position.y)
            {
                floorNumber = i;
            }
        }
        if (floorNumber < 0)
        {
            SetPlayerToFloor(0);
            floorNumber = 0;
        }
        return floorNumber;
    }

    void SetPlayerToFloor(int _floor)
    {
        Vector3 playerPos = mainCamera.transform.position;
        mainCamera.transform.Translate(0, platformScript.points[_floor].position.y - playerPos.y, 0);
    }
}
