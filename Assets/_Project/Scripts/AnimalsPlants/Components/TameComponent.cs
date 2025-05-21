using UnityEngine;

namespace _Project.Scripts.AnimalsPlants.Components
{
    public class TameComponent : MonoBehaviour
    {
        public Item requiredFood;
        private bool isTamed;

        public void TryTame(PlayerController player)
        {
            if (!isTamed && Input.GetKeyDown(KeyCode.E) && player.itemInHands?.itemType == ItemType.Food && player.itemInHands == requiredFood)
            {
                isTamed = true;
                var follow = gameObject.AddComponent<FollowComponent>();
                follow.SetTarget(player.transform);
                Debug.Log($"{gameObject.name} tamed!");
            }
        }
    }
}