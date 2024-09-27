using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalOrPlant : MonoBehaviour
{
    public AnimalPlantScriptableObject animalPlant;
    public float probabilityOfOccurence;
    public Attacker GetAttacker()
    {
        return animalPlant as Attacker;
    }
}
