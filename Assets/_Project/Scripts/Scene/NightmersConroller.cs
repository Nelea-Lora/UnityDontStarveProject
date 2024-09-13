using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightmersConroller : MonoBehaviour
{
    [SerializeField] private DayTime _day;
    [SerializeField] private HealthSystem _health;
    [SerializeField] private PlayerController _playerController;
    private bool _pointLightStatus;
    void Update()
    {
        if (_day is not null && _day.DayProgress() > 0.4 && !_pointLightStatus)
        {
            _health.TakeDamage(0.0002f);
        }
        if (_playerController && _playerController.itemInHands)
        {
            if(_playerController.itemInHands.itemType == ItemType.Light) _pointLightStatus = true;
            else _pointLightStatus = false;
        }
    }
}
