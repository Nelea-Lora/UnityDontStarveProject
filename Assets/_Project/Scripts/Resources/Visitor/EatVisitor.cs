using UnityEngine;

namespace _Project.Scripts.Resources.Visitor
{
    public class EatVisitor : IItemVisitor
    {
        public EatVisitor()
        {
            
        }

        public void Visit(FoodItem food)
        {
            // GameFacade.Instance.Heal(food.healingAmount);
            // GameFacade.Instance.Eat(food.eatingAmount);
            // GameFacade.Instance.IncreaseMind(food.mindAmount);
            food.ApplyEffect();
            GameFacade.Instance.UseItemAndDecrease();
        }

        public void Visit(BuildItem build)
        {
            Debug.Log("Нельзя это есть!");
        }
        public void Visit(InstrumentItem instrument)
        {
            Debug.Log("Нельзя это есть!");
        }
        public void Visit(MaterialItem material)
        {
            Debug.Log("Нельзя это есть!");
        }
        public void Visit(LightItem light)
        {
            Debug.Log("Нельзя это есть!");
        }
    }

}