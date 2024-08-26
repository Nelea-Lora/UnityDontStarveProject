using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorBoundaries : MonoBehaviour
{
    public static float horizontalLimitValue;
    public static float verticalLimitValue;
    public GameObject floorObject;
    
    void Awake()
    {
        Renderer floorRenderer = floorObject.GetComponent<Renderer>();
        if (floorRenderer != null)
        {
            Vector3 floorSize = floorRenderer.bounds.size;
            horizontalLimitValue = floorSize.x / 2f;
            verticalLimitValue = floorSize.y / 2f;
        }
    }
}
