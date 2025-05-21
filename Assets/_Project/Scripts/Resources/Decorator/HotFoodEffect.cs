using UnityEngine;

namespace _Project.Scripts.Resources.Decorator
{
    [CreateAssetMenu(menuName = "Inventory/Food/Hot Food Effect")]
    public class HotFoodEffect: FoodEffectDecorator
    {
        public float hotScore;

        public override void ApplyEffect(FoodItem food)
        {
            base.ApplyEffect(food);
            GameFacade.Instance.Eat(-hotScore);
            Debug.Log("Hot effect applied!");
        }
    }
}