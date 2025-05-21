using UnityEngine;

namespace _Project.Scripts.Resources.Decorator
{
    [CreateAssetMenu(menuName = "Inventory/Food/Poisoned Food Effect")]
    public class PoisonedFoodEffect : FoodEffectDecorator
    {
        public float poisonDamage;

        public override void ApplyEffect(FoodItem food)
        {
            base.ApplyEffect(food);
            //healthSystem.TakeDamage(poisonDamage);
            GameFacade.Instance.TakeDamage(poisonDamage);
            Debug.Log("Poison effect applied!");
        }
    }

}