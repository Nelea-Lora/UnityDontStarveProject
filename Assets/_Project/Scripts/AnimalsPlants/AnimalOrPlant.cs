using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.AnimalsPlants.Components;
using UnityEngine;

public class AnimalOrPlant : MonoBehaviour
{
    public AnimalPlantScriptableObject animalPlant;
    public float probabilityOfOccurence;
    
    private void Start()
    {
        if (animalPlant != null)
        {
            // GetComponent<FruitDropComponent>()?.Initialize(animalPlant.items, (animalPlant as FruitDroppingPlant)?.dropChance ?? 0.1f);
        }
    }
}
