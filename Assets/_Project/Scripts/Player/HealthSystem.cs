using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private Image _health;
    [SerializeField] private Image _hunger;
    [SerializeField] private Image _mind;
    [SerializeField] private float fill;
    void Start()
    {
        fill = 1f;
    }
    
    void Update()
    {
        _health.fillAmount = fill;
    }
}
