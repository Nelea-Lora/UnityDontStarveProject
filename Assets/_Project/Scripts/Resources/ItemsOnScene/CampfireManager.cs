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
    private float _fireLife;
    [SerializeField] private Transform player;
    internal float DistanceToPlayer;
    public ItemScriptableObject _item;//private
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        //_item = Instantiate(_Item);
        _item = _Item;
        _item.currTimeShelfLife = _item.maxTimeShelfLife;
        // levelFire = maxLevelFire;
        // _tmpTimeForFire = 0.1f;
        // _fireLife = levelFire / 2;
         _fireLife = _item.currTimeShelfLife / 2;
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _light = GetComponentInChildren<Light>();
        _light.range = 20;
    }
    
    void Update()
    {
        DistanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (_tmpTimeForFire < Time.time && _item && _item.currTimeShelfLife>0) ChangeFire();
        //if (_tmpTimeForFire < Time.time && levelFire>0) ChangeFire();
    }

    private void ChangeFire()
    {
        if(_item.maxTimeShelfLife<=0 &&!_item.icon)return;
        if (_item.currTimeShelfLife > _fireLife)
        {
            _item.currTimeShelfLife -= 0.1f;
            _spriteRenderer.sprite = _fire;
        }
        if (_item.currTimeShelfLife <= _fireLife)
        {
            _spriteRenderer.sprite = _middleFire;
            _item.currTimeShelfLife -= 0.1f;
            _light.range = 5;
        }
        if (_item.currTimeShelfLife <= 0)
        {
            _spriteRenderer.sprite = _offFire;
            _light.range = 0;
        }
        // if (levelFire > _fireLife)
        // {
        //     levelFire -= 0.1f;
        //     _spriteRenderer.sprite = _fire;
        // }
        // if (levelFire <= _fireLife)
        // {
        //     _spriteRenderer.sprite = _middleFire;
        //     levelFire -= 0.1f;
        //     _light.range = 5;
        // }
        // if (levelFire <= 0)
        // {
        //     _spriteRenderer.sprite = _offFire;
        //     _light.range = 0;
        // }
        _tmpTimeForFire += 0.5f;
    }

    public void ChangeFireLevel(float amount)
    {
        _item.currTimeShelfLife += amount;
        //levelFire += amount;
    }
}
