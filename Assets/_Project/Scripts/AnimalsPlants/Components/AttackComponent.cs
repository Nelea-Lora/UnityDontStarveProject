using UnityEngine;

namespace _Project.Scripts.AnimalsPlants.Components
{
    public class AttackComponent : MonoBehaviour
    {
        public float damageAmount;

        public void TriggerAttack(PlayerController player)
        {
            GameFacade.Instance.TakeDamage(damageAmount);
        }
    }
}