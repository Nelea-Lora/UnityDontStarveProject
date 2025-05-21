using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimalPlantType {Attacker, Runner, Vegetable, TamableAnimal, FruitDroppingPlant}
public class AnimalPlantScriptableObject : ScriptableObject
{
    public GameObject animalPlantPrefab;
    public AnimalPlantType animalPlantType;
    public string animalPlantDescription;
    public Sprite icon;
    public float healthLevel;
    public float maxHealthLevel;
    public List<Item> items = new List<Item>();
}
