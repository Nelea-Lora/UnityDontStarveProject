using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLifeCycles : MonoBehaviour
{
    [SerializeField] private InventoryManager _inventoryManager;
    [SerializeField] private CampfireManager _campfireManager;
    [SerializeField] private PlayerController _playerController;
    void Update()
    {
        if (_playerController && _playerController.itemInHands && _playerController.itemInHands.burnLevel>0
            && _campfireManager&& _campfireManager._item && _inventoryManager
            && _inventoryManager.itemTmp&& _inventoryManager.itemTmp._Item
            && _inventoryManager.itemTmp._Item==_campfireManager._item&& Input.GetKeyDown(KeyCode.Space)) 
            UpdateLifeCycles();
    }

    private void UpdateLifeCycles()
    {
        print(" UpdateLifeCycles ");
        if (_campfireManager._item.itemType == ItemType.BuildItem)
        {
            BuildItem buildItem = _campfireManager._item as BuildItem;
            if(!buildItem)return;
            print(" buildItem ");
            if (buildItem.buildItemType == ItemOnSceneType.Campfire)
            {
                _campfireManager.ChangeFireLevel(_playerController.itemInHands.burnLevel);
                print(" Campfire ");
                print("*********levelFire********* "+ _campfireManager.levelFire);
            }
        }
        
    }
}
