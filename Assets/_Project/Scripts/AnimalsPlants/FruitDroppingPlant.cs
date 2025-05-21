using UnityEngine;

[CreateAssetMenu(fileName = "FruitDroppingPlant", menuName = "Animals Plants/New Fruit Dropping Plant")]
public class FruitDroppingPlant : AnimalPlantScriptableObject
{
    public float dropChance = 0.1f; // Шанс падения плода за кадр

    private void OnEnable()
    {
        animalPlantType = AnimalPlantType.FruitDroppingPlant;
    }
}