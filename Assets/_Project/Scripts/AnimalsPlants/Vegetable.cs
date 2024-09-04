using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Vegetable",menuName = "Animals Plants/New Vegetable")]
public class Vegetable : AnimalPlantScriptableObject
{
    public InstrumentType instrumentType;
    private void Start()
    {
        animalPlantType = AnimalPlantType.Vegetable;
    } 
}
