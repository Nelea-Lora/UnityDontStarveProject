using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private Vector3 _movingVector;
    private Vector3 _movingClick;
    private float _horizontalLimitValue;
    private float _verticalLimitValue;
    private bool _isChecked;
    public ItemScriptableObject itemInHands;
    private Camera _camera;
    private bool _isMoving;

    void Start()
    {
        _camera = Camera.main;
        _horizontalLimitValue = FloorBoundaries.horizontalLimitValue;
        _verticalLimitValue = FloorBoundaries.verticalLimitValue;
    }
    
    void Update()
    {
        float axis;
            if (Input.GetAxis("Horizontal") != 0)
            {
                axis = Input.GetAxis("Horizontal");
                _movingVector = new Vector3(axis * speed, 0, 0);
                MovingWasd();
            }
            else if (Input.GetAxis("Vertical") != 0)
            {
                axis = Input.GetAxis("Vertical");
                _movingVector = new Vector3(0, axis * speed, 0);
                MovingWasd();
            }
    }

    private void MovingWasd()
    {
        Vector3 newPosition = transform.position + _movingVector * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, -_horizontalLimitValue, _horizontalLimitValue);
        newPosition.y = Mathf.Clamp(newPosition.y, -_verticalLimitValue, _verticalLimitValue);
        transform.position = newPosition;
    }
}
