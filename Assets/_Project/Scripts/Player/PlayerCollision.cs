using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public Action<Item> OnItemTriggerEnter;
    public Action<Item> OnItemTriggerExit;
    public Action<AnimalOrPlant> OnAnimalOrPlantTriggerEnter;
    public Action<AnimalOrPlant> OnAnimalOrPlantTriggerExit;
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Item item))
        {
            OnItemTriggerEnter?.Invoke(item);
        }
        else if (other.TryGetComponent(out AnimalOrPlant animalOrPlant))
        {
            OnAnimalOrPlantTriggerEnter?.Invoke(animalOrPlant);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Item item))
        {
            OnItemTriggerExit?.Invoke(item);
        }
        else if (other.TryGetComponent(out AnimalOrPlant animalOrPlant))
        {
            OnAnimalOrPlantTriggerExit?.Invoke(animalOrPlant);
        }
    }
}
