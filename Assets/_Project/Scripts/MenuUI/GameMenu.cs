using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public bool Select { get; private set; }

    public void SelectAnItemFromTheInventory()
    {
        if (Select == false) Select = true;
        else Select = false;
    }
}
