using UnityEngine;

namespace _Project.Scripts.Resources.Decorator
{
    public abstract class FoodEffectDecorator : FoodEffect
    {
        [SerializeField] protected FoodEffect wrappedEffect;

        public override void ApplyEffect(FoodItem food)
        {
            if (wrappedEffect != null)
                wrappedEffect.ApplyEffect(food);
        }
    }

}