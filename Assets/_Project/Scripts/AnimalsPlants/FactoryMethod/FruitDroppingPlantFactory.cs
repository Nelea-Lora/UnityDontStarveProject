using _Project.Scripts.AnimalsPlants.Components;
using UnityEngine;

namespace _Project.Scripts.AnimalsPlants.FactoryMethod
{
    public class FruitDroppingPlantFactory : AnimalPlantFactory
    {
        public override AnimalOrPlant CreateAnimalOrPlant(AnimalPlantScriptableObject data, Vector3 position)
        {
            var instance = Object.Instantiate(data.animalPlantPrefab, position, Quaternion.identity);
            var animalOrPlant = instance.GetComponent<AnimalOrPlant>();
            animalOrPlant.animalPlant = data;
            var fruitDrop = instance.AddComponent<FruitDropComponent>();
            fruitDrop.fruit = data.items[0]; // Предполагается, что первый предмет — плод
            fruitDrop.dropChance = (data as FruitDroppingPlant).dropChance;
            return animalOrPlant;
        }
    }
}