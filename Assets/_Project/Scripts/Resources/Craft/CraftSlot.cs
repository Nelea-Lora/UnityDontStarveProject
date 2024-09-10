using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftSlot : MonoBehaviour
{
    public ItemScriptableObject item;
    private void Start()
    {
        transform.GetChild(0).GetComponent<Image>().sprite = item.icon;
    }
}
