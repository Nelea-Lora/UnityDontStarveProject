using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    public float length;
    public float weight;
    void Start()
    {
        length = transform.localScale.y;
    }
}
