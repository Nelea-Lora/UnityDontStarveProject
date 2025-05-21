using UnityEngine;

[CreateAssetMenu(fileName = "TamableAnimal", menuName = "Animals Plants/New Tamable Animal")]
public class TamableAnimal : AnimalPlantScriptableObject
{
    public Item requiredFood; // Еда, необходимая для приручения

    private void OnEnable()
    {
        animalPlantType = AnimalPlantType.TamableAnimal;
    }
}