using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftPanel : MonoBehaviour
{
    [SerializeField] private GameObject _craftPanel;
    //public bool create;
    public bool Сreate { get;private set; }
    
    void Awake()
    {
        _craftPanel.SetActive(false);
    }

    public void OpenCloseCraftPanel()
    {
        if(_craftPanel.activeInHierarchy)_craftPanel.SetActive(false);
        else _craftPanel.SetActive(true);
    }

    public void CreateItem()
    {
        Сreate = true;
    }
    public void ItemCreated()
    {
        Сreate = false;
    }
}
