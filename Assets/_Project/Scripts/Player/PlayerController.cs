using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    private Vector3 _movingVector;
    private float _horizontalLimitValue;
    private float _verticalLimitValue;
    //public ButtonsStartMenu _buttonSrart;
    private bool _isChecked;
    void Start()
    {
        _horizontalLimitValue = FloorBoundaries.horizontalLimitValue;
        _verticalLimitValue = FloorBoundaries.verticalLimitValue;
        //Time.timeScale = 0f;
        // _buttonSrart.StartGameOn();
        // if (Input.GetMouseButton(0) && !_buttonSrart.IsStarted)
        // {
        //     _buttonSrart.StartGame();
        // }
    }
    
    void Update()
    {
        float axis;
        // if (!_isChecked)
        // {
        //     Time.timeScale = 1f;
        //     _isChecked = true;
        //     
        // }
        
        // if (_buttonSrart.IsStarted && _isChecked)
        // {
            if (Input.GetAxis("Horizontal") != 0)
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
            newPosition.x = Mathf.Clamp(newPosition.x, -_horizontalLimitValue, _horizontalLimitValue);
            newPosition.z = Mathf.Clamp(newPosition.z, -_verticalLimitValue, _verticalLimitValue);
            transform.position = newPosition;
        //}
    }
}
