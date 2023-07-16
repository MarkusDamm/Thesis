using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScaler : MonoBehaviour
{
    Vector3 startPosition;
    Vector3 startSize;
    Terrain terrain;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = gameObject.transform.position;
        terrain = gameObject.GetComponent<Terrain>();
        startSize = terrain.terrainData.size;
    }

    public void scale(float _multiplier)
    {
        Vector3 sizeScaler = terrain.terrainData.size * _multiplier;
        terrain.terrainData.size = sizeScaler;
        Vector3 positionScaler = gameObject.transform.position * _multiplier;
        gameObject.transform.position = positionScaler;
    }

    public void restore()
    {
        terrain.terrainData.size = startSize;
        gameObject.transform.position = startPosition;
    }
}
