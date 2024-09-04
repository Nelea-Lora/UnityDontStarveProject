using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Attacker",menuName = "Animals Plants/New Attacker")]
public class Attacker : AnimalPlantScriptableObject
{
    public float damageAmount;
    private void Start()
    {
        animalPlantType = AnimalPlantType.Attaker;
    } 
}
