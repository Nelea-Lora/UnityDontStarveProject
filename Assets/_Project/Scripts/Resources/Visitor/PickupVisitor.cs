using UnityEngine;

namespace _Project.Scripts.Resources.Visitor
{
    public class PickupVisitor : IItemVisitor
    {
        private InventoryManager inventory;
        private Item itemComponent;

        public PickupVisitor(InventoryManager inv, Item item)
        {
            inventory = inv;
            itemComponent = item;
        }

        public void Visit(FoodItem food)
        {
            inventory.AddItem(food, itemComponent._amount);
            GameObject.Destroy(itemComponent.gameObject);
        }

        public void Visit(BuildItem build)
        {
            Debug.Log("Нельзя поднять строительный предмет!");
        }
        public void Visit(InstrumentItem instrument)
        {
            inventory.AddItem(instrument, itemComponent._amount);
            GameObject.Destroy(itemComponent.gameObject);
        }
        public void Visit(MaterialItem material)
        {
            inventory.AddItem(material, itemComponent._amount);
            GameObject.Destroy(itemComponent.gameObject);
        }
        public void Visit(LightItem light)
        {
            inventory.AddItem(light, itemComponent._amount);
            GameObject.Destroy(itemComponent.gameObject);
        }
    }

}