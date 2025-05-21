using UnityEngine;

namespace _Project.Scripts.Resources.Decorator
{
    public abstract class FoodEffect : ScriptableObject
    {
        public abstract void ApplyEffect(FoodItem food);
    }

}