using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class CampfireManager : Item
{
    [SerializeField] private Sprite _fire;
    [SerializeField] private Sprite _middleFire;
    [SerializeField] private Sprite _offFire;
    public float maxLevelFire;
    public float levelFire;
    private Light _light;
    private SpriteRenderer _spriteRenderer;
    private float _tmpTimeForFire;
    [SerializeField] private Transform player;
    internal float DistanceToPlayer;

    private float _remainingBurningTime;
    void Start()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
        
        _remainingBurningTime = maxLevelFire;
        
        levelFire = maxLevelFire;
        _tmpTimeForFire = 0.1f;
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _light = GetComponentInChildren<Light>();
        _light.range = 20;
    }
    
    void Update()
    {
        DistanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (_tmpTimeForFire < Time.time) ChangeFire();
        if (_remainingBurningTime>0)Burn(-Time.deltaTime);
    }

    public float Burn(float amount)
    {
        _remainingBurningTime = Mathf.Clamp(_remainingBurningTime + amount, 0, maxLevelFire);
        if(!_light) return _remainingBurningTime;
        _light.intensity = Mathf.Lerp(0, 5, _remainingBurningTime / maxLevelFire);
        _light.range = Mathf.Lerp(0, 5, _remainingBurningTime / maxLevelFire);
        return _remainingBurningTime;
    }

    private void ChangeFire()
    {
        if(!_spriteRenderer) return;
        if (_remainingBurningTime > maxLevelFire / 2f)
        {
           // levelFire -= 0.1f;
            _spriteRenderer.sprite = _fire;
        }
        if (_remainingBurningTime <= maxLevelFire / 2f)
        {
            _spriteRenderer.sprite = _middleFire;
        //    levelFire -= 0.1f;
        //    _light.range = 5;
        }
        if (_remainingBurningTime <= 0)
        {
            _spriteRenderer.sprite = _offFire;
        //    _light.range = 0;
        }
        _tmpTimeForFire += 0.5f;
    }
}
