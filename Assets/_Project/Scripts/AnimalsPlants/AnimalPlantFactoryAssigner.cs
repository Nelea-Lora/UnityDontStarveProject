using System.Collections.Generic;
using _Project.Scripts.AnimalsPlants.FactoryMethod;
using UnityEngine;

public class AnimalPlantFactoryAssigner
{
    private readonly Dictionary<AnimalPlantType, AnimalPlantFactory> _factories;

    public AnimalPlantFactoryAssigner()
    {
        _factories = new Dictionary<AnimalPlantType, AnimalPlantFactory>
        {
            { AnimalPlantType.Attacker, new AttackerFactory() },
            { AnimalPlantType.Vegetable, new VegetableFactory() },
            { AnimalPlantType.TamableAnimal, new TamableAnimalFactory() },
            { AnimalPlantType.FruitDroppingPlant, new FruitDroppingPlantFactory() }
        };
    }

    public void AssignFactory(ref AnimalPlantEntry entry)
    {
        if (entry.data == null)
        {
            Debug.LogError("AnimalPlantEntry data is null! Cannot assign factory.");
            entry.factory = null;
            return;
        }

        if (_factories.TryGetValue(entry.data.animalPlantType, out var factory))
        {
            entry.factory = factory;
        }
        else
        {
            Debug.LogError($"No factory found for AnimalPlantType: {entry.data.animalPlantType}");
            entry.factory = null;
        }
    }
}