using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftPanel : MonoBehaviour
{
    [SerializeField] private GameObject _craftPanel;
    public bool Сreate { get;private set; }
    
    void Awake()
    {
        _craftPanel.SetActive(false);
    }

    public void OpenCloseCraftPanel()
    {
        if(_craftPanel.activeInHierarchy)_craftPanel.SetActive(false);
        else _craftPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void CreateItem()
    {
        Сreate = true;
        EventSystem.current.SetSelectedGameObject(null);
    }
    public void ItemCreated()
    {
        Сreate = false;
    }
}
