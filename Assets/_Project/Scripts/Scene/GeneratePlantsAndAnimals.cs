using System.Collections.Generic;
using _Project.Scripts.AnimalsPlants.FactoryMethod;
using UnityEngine;

[System.Serializable]
public struct AnimalPlantEntry
{
    public AnimalPlantFactory factory;
    public AnimalPlantScriptableObject data;
    public float probabilityOfOccurence;
}

public class GeneratePlantsAndAnimals : MonoBehaviour
{
    [SerializeField] private int _countOfAnimalOrPlant = 10;
    [SerializeField] private AnimalPlantEntry[] _animalPlantEntries;
    private float[] _weights;
    private int _index;
    private AnimalPlantFactoryAssigner _factoryAssigner;

    void Awake()
    {
        _factoryAssigner = new AnimalPlantFactoryAssigner();
    }

    void Start()
    {
        // Validate _animalPlantEntries
        if (_animalPlantEntries == null || _animalPlantEntries.Length == 0)
        {
            Debug.LogError("AnimalPlantEntries is empty or not assigned in the Inspector!");
            return;
        }

        // Assign factories to each entry
        for (int i = 0; i < _animalPlantEntries.Length; i++)
        {
            var entry = _animalPlantEntries[i];
            _factoryAssigner.AssignFactory(ref entry);
            _animalPlantEntries[i] = entry; // Обновляем массив
        }
        _weights = new float[_animalPlantEntries.Length];
        for (int i = 0; i < _animalPlantEntries.Length; i++)
        {
            if (_animalPlantEntries[i].factory == null || _animalPlantEntries[i].data == null)
            {
                Debug.LogWarning($"AnimalPlantEntry at index {i} has unassigned factory or data!");
                _weights[i] = 0f;
            }
            else
            {
                _weights[i] = _animalPlantEntries[i].probabilityOfOccurence;
            }
        }
        
        for (int i = 0; i < _countOfAnimalOrPlant; i++)
        {
            SpawnAnimalOrPlant();
        }
    }

    public void SpawnAnimalOrPlant()
    {
        float randoPositionX = Random.Range(-45f, 45f);
        float randoPositionY = Random.Range(-45f, 45f);
        Vector3 cubePosition = new Vector3(randoPositionX, randoPositionY, -0.5f);
        
        float totalWeight = 0f;
        foreach (float weight in _weights)
        {
            totalWeight += weight;
        }
        
        if (totalWeight <= 0f)
        {
            Debug.LogWarning("Total weight is zero or negative. Cannot spawn animal or plant.");
            return;
        }
        
        float randomValue = Random.Range(0f, totalWeight);
        float cumulative = 0f;
        _index = -1; // Default to invalid index
        for (int i = 0; i < _weights.Length; i++)
        {
            cumulative += _weights[i];
            if (randomValue <= cumulative && _weights[i] > 0f)
            {
                _index = i;
                break;
            }
        }
        
        if (_index < 0 || _animalPlantEntries[_index].factory == null || _animalPlantEntries[_index].data == null)
        {
            Debug.LogWarning("Failed to select a valid AnimalPlantEntry for spawning.");
            return;
        }
        
        _animalPlantEntries[_index].factory.CreateAnimalOrPlant(
            _animalPlantEntries[_index].data,
            cubePosition
        );
    }
}