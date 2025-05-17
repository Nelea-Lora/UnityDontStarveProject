using UnityEngine;

namespace _Project.Scripts.Resources.Visitor
{
    public class BurnVisitor : IItemVisitor
    {
        public void Visit(FoodItem food)
        {
            Debug.Log("Можно готовить еду");
            var player = GameFacade.Instance.GetPlayer();
            GameFacade.Instance.GetRecipeManager().Cook(player.itemInHands);
            Debug.Log("Приготовили еду");
        }

        public void Visit(BuildItem build)
        {
            Debug.Log("Этот предмет нельзя сжечь!");
        }
        public void Visit(InstrumentItem instrument)
        {
            Debug.Log("Этот предмет нельзя сжечь!");
        }
        public void Visit(MaterialItem material)
        {
            if (material.burnLevel > 0)
            {
                Debug.Log("Сжигаем материал");
                var player = GameFacade.Instance.GetPlayer();
                var campfireManager = GameFacade.Instance.GetObjectLifeCycles().GetCampfire();
                float burn = campfireManager.
                    Burn(campfireManager.maxLevelFire / player.itemInHands.burnLevel);
                Debug.Log("burnLevel "+burn);
            }
            else
            {
                Debug.Log("Этот предмет нельзя сжечь!");
            }
        }
        public void Visit(LightItem light)
        {
            if (light.burnLevel > 0)
            {
                Debug.Log("Сжигаем источник света");
                var player = GameFacade.Instance.GetPlayer();
                var campfireManager = GameFacade.Instance.GetObjectLifeCycles().GetCampfire();
                float burn = campfireManager.
                    Burn(campfireManager.maxLevelFire / player.itemInHands.burnLevel);
                Debug.Log("burnLevel "+burn);
            }
            else
            {
                Debug.Log("Этот предмет нельзя сжечь!");
            }
        }
    }

}