using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private Vector3 _movingVector;
    private float _horizontalLimitValue;
    private float _verticalLimitValue;
    private bool _isChecked;
    public ItemScriptableObject itemInHands;
    [SerializeField] private DayTime day;
    [SerializeField] private Sprite bosorkaDay;
    [SerializeField] private Sprite bosorkaNight;
    private SpriteRenderer _spriteRenderer;
    void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (_spriteRenderer != null) _spriteRenderer.sprite = bosorkaDay;
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
        if (day && day.DayProgress() > 0.5f && day.DayProgress() < 0.6f) 
            _spriteRenderer.sprite = bosorkaNight;
        if (day && day.DayProgress() == 0f) _spriteRenderer.sprite = bosorkaDay;
    }

    private void MovingWasd()
    {
        Vector3 newPosition = transform.position + _movingVector * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, -_horizontalLimitValue, _horizontalLimitValue);
        newPosition.y = Mathf.Clamp(newPosition.y, -_verticalLimitValue, _verticalLimitValue);
        transform.position = newPosition;
    }
}
