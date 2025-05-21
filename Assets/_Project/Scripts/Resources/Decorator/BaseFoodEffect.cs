using UnityEngine;

namespace _Project.Scripts.Resources.Decorator
{
    [CreateAssetMenu(menuName = "Inventory/Food/Base Food Effect")]
    public class BaseFoodEffect : FoodEffect
    {   
        public override void ApplyEffect(FoodItem food)
        {
            GameFacade.Instance.Heal(food.healingAmount);
            GameFacade.Instance.Eat(food.eatingAmount);
            GameFacade.Instance.IncreaseMind(food.mindAmount);
            Debug.Log("Base food effect applied!");
        }
    }

}