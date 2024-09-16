using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightmersConroller : MonoBehaviour
{
    [SerializeField] private DayTime _day;
    [SerializeField] private HealthSystem _health;
    [SerializeField] private PlayerController _playerController;
    public bool PointLightStatus { get; private set; }
    
    void Update()
    {
        if (_day is not null && _day.DayProgress() > 0.4 && !PointLightStatus)
        {
            _health.TakeDamage(0.0002f);
        }
        if (_playerController && _playerController.itemInHands
                              && _playerController.itemInHands.itemType == ItemType.Light) 
            PointLightStatus = true;
        else PointLightStatus = false;
    }
}
