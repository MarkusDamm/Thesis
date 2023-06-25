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
        currentFloor = estimateFloor();
    }

    // Update is called once per frame
    void Update()
    {
        currentFloor = estimateFloor();
        Debug.Log("Current Floor: " + currentFloor + ".  Last Floor: " + lastFloor);
        // if (currentFloor != lastFloor)
        // {
        if (!platformScript.canMove && platformScript.currentLevel != currentFloor)
        {
            platformScript.MoveToLevel(currentFloor);
            lastFloor = currentFloor;
        }

        // }
    }

    int estimateFloor()
    {
        float currentHeigth = mainCamera.transform.position.y;
        // bool floorIsEstimated = false;
        int floorNumber = -1;
        for (int i = 0; i < platformScript.points.Length; i++)
        {
            if (currentHeigth > platformScript.points[i].position.y)
            {
                // floorIsEstimated = true;
                floorNumber = i;
                // return floorNumber;
            }
        }
        if (floorNumber < 0)
        {
            setPlayerToFloor(0);
            floorNumber = 0;
        }
        return floorNumber;
    }

    void setPlayerToFloor(int _floor)
    {
        Vector3 playerPos = mainCamera.transform.position;
        // playerOrigin.transform.position.Set(playerPos.x, platformScript.points[_floor].position.y + 1, playerPos.z);
        mainCamera.transform.Translate(0, platformScript.points[_floor].position.y - playerPos.y, 0);
    }
}
