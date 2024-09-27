using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateGround : MonoBehaviour
{
    [SerializeField] private float _length;
    [SerializeField] private float _lengthBoundaries;
    [SerializeField] private FloorController[] _floorPrefabs;
    [SerializeField] private FloorController[] _darkFloorPrefabs;
    private float[] _weights; 
    private float[] _weightsDarkFloor; 
    private int _floorCount;
    private int _floorIndex;
    private Vector2 _startPosition = new Vector2(-47f, -47f);
    
    void Start()
    {
        _weights = new float[_floorPrefabs.Length];
        SetWeights(_weights, _floorPrefabs);
        _weightsDarkFloor = new float[_darkFloorPrefabs.Length];
        SetWeights(_weightsDarkFloor, _darkFloorPrefabs);
        _floorCount = Mathf.FloorToInt(_lengthBoundaries / _length);
        for (int x = 0; x < _floorCount; x++)
        {
            for (int y = 0; y < _floorCount; y++)
            {
                float randomValue = Random.Range(0, _floorCount);
                if(y > (x-randomValue) || y < randomValue) 
                    SpawnDarkGround(x,y);
                else SpawnGround(x,y);
            }
        }
    }
    private void SpawnGround(int indexX, int indexY)
    {
        float posX = _startPosition.x+indexX * _length;
        float posY = _startPosition.y+indexY * _length;
        Vector3 cubePosition = new Vector3(posX, posY, 0);
        float totalWeight = 0f;
        foreach (float weight in _weights)
        {
            totalWeight += weight;
        }
        float randomValue = Random.Range(0, totalWeight);
        for (int i = 0; i < _weights.Length; i++)
        {
            if (randomValue < _weights[i])
            {
                _floorIndex = i;
                break;
            }
            randomValue -= _weights[i];
        }
        Instantiate(_floorPrefabs[_floorIndex], cubePosition, Quaternion.identity);
    }
    private void SpawnDarkGround(int indexX, int indexY)
    {
        float posX = _startPosition.x+indexX * _length;
        float posY = _startPosition.y+indexY * _length;
        Vector3 cubePosition = new Vector3(posX, posY, 0);
        float totalWeight = 0f;
        foreach (float weight in _weightsDarkFloor)
        {
            totalWeight += weight;
        }
        float randomValue = Random.Range(0, totalWeight);
        for (int i = 0; i < _weightsDarkFloor.Length; i++)
        {
            if (randomValue < _weightsDarkFloor[i])
            {
                _floorIndex = i;
                break;
            }
            randomValue -= _weightsDarkFloor[i];
        }
        Instantiate(_darkFloorPrefabs[_floorIndex], cubePosition, Quaternion.identity);
    }

    private void SetWeights(float[] weights, FloorController[] floor)
    {
        for (int i = 0; i < floor.Length; i++)
        {
            weights[i] = floor[i].weight;
        }
    }
}
