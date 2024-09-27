using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePlantsAndAnimals : MonoBehaviour
{
    [SerializeField] private AnimalOrPlant[] _animalOrPlantsPrefabs;
    [SerializeField] private int _countOfAnimalOrPlant;
    private float[] _weights;
    private int _index;
    void Start()
    {
        _weights = new float[_animalOrPlantsPrefabs.Length];
        for (int i = 0; i < _animalOrPlantsPrefabs.Length; i++)
        {
            _weights[i] = _animalOrPlantsPrefabs[i].probabilityOfOccurence;
        }

        for (int i = 0; i < _countOfAnimalOrPlant; i++)
        {
            SpawnAnimalOrPlant();
            
        }
    }

    public void SpawnAnimalOrPlant()
    {
        float randoPositionX = Random.Range(-45, +45);
        float randoPositionY = Random.Range(-45, +45);
        Vector3 cubePosition = new Vector3(randoPositionX, randoPositionY, -0.5f);
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
                _index = i;
                break;
            }
            randomValue -= _weights[i];
        }
        Instantiate(_animalOrPlantsPrefabs[_index], cubePosition, Quaternion.identity);
    }
}
