using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightmersConroller : MonoBehaviour
{
    [SerializeField] private DayTime _day;
    [SerializeField] private HealthSystem _health;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private PlayerCollision _playerCollision;
    private CampfireManager _campfireManager ;
    public bool PointLightStatus { get; private set; }

    void Start()
    {
        _playerCollision.OnItemTriggerEnter += CampfireEnter;
    }
    void Update()
    {
        if ((_playerController && _playerController.itemInHands
                               && _playerController.itemInHands.itemType == ItemType.Light)
                               || (_campfireManager && _campfireManager.DistanceToPlayer < 10)) 
            PointLightStatus = true;
        else PointLightStatus = false;
        if (_day is not null && _day.DayProgress() > 0.4 && !PointLightStatus)
        {
            _health.TakeDamage(0.0002f);
        }
    }
    private void CampfireEnter(Item item)
    {
        if (item.TryGetComponent(out CampfireManager campfireManager))
        {
            _campfireManager = campfireManager;
        }
    }
}
