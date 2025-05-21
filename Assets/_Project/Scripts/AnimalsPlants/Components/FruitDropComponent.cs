using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.AnimalsPlants.Components
{
    public class FruitDropComponent : MonoBehaviour
    {
        public Item fruit;
        public float dropChance;

        private void Update()
        {
            if (Random.value < dropChance * Time.deltaTime)
            {
                Instantiate(fruit.gameObject, transform.position, Quaternion.identity);
            }
        }

        // public void Initialize(List<Item> animalPlantItems, float f)
        // {
        //     throw new System.NotImplementedException();
        // }
    }
}