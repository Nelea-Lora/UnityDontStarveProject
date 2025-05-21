using UnityEngine;

namespace _Project.Scripts.AnimalsPlants.FactoryMethod
{
    public abstract class AnimalPlantFactory
    {
        public abstract AnimalOrPlant CreateAnimalOrPlant(AnimalPlantScriptableObject data, Vector3 position);
    }
}