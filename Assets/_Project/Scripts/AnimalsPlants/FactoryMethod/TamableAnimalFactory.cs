using _Project.Scripts.AnimalsPlants.Components;
using UnityEngine;

namespace _Project.Scripts.AnimalsPlants.FactoryMethod
{
    public class TamableAnimalFactory : AnimalPlantFactory
    {
        public override AnimalOrPlant CreateAnimalOrPlant(AnimalPlantScriptableObject data, Vector3 position)
        {
            var instance = Object.Instantiate(data.animalPlantPrefab, position, Quaternion.identity);
            var animalOrPlant = instance.GetComponent<AnimalOrPlant>();
            animalOrPlant.animalPlant = data;
            var tame = instance.AddComponent<TameComponent>();
            tame.requiredFood = (data as TamableAnimal).requiredFood;
            return animalOrPlant;
        }
    }
}