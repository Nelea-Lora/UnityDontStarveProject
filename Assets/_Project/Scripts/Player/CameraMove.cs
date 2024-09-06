using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform player;
    private Vector3 _offset;
    public float smoothSpeed = 0.225f;

    void Start()
    {
        _offset = transform.position - player.transform.position;
    }
    void Update()
    {
        Vector3 desiredPosition = player.position + _offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}
