using UnityEngine;

namespace _Project.Scripts.AnimalsPlants.FactoryMethod
{
    public class VegetableFactory : AnimalPlantFactory
    {
        public override AnimalOrPlant CreateAnimalOrPlant(AnimalPlantScriptableObject data, Vector3 position)
        {
            var instance = Object.Instantiate(data.animalPlantPrefab, position, Quaternion.identity);
            var animalOrPlant = instance.GetComponent<AnimalOrPlant>();
            animalOrPlant.animalPlant = data;
            return animalOrPlant;
        }
    }
}