using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    private Vector3 _movingVector;
    private float horizontalLimitValue;
    private float verticalLimitValue;
    void Start()
    {
        horizontalLimitValue = FloorBoundaries.horizontalLimitValue;
        verticalLimitValue = FloorBoundaries.verticalLimitValue;
    }
    
    void Update()
    {
        float axis;
        if(Input.GetAxis("Horizontal") != 0)
        {
            axis = Input.GetAxis("Horizontal");
            _movingVector = new Vector3(axis * _speed, 0, 0);
        }
        else
        {
            axis = Input.GetAxis("Vertical");
            _movingVector = new Vector3(0, 0, axis * _speed);
        }
        Vector3 newPosition = transform.position + _movingVector * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, -horizontalLimitValue, horizontalLimitValue);
        newPosition.z = Mathf.Clamp(newPosition.z, -verticalLimitValue, verticalLimitValue);
        transform.position = newPosition;
   }
}
