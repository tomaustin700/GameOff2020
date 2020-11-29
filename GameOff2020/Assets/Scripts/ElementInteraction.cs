using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementInteraction : MonoBehaviour
{

    public void PickUpElement()
    {
        var formattedName = name.Replace("(Clone)", "");
        var item = new InventoryItem((Item)Enum.Parse(typeof(Item), formattedName, true));
        var hotbar = GameObject.FindGameObjectWithTag("Hotbar").GetComponent<InventoryManager>();
        if (hotbar.HasInventorySpace())
        {
            GameObject.FindGameObjectWithTag("Hotbar").GetComponent<InventoryManager>().AddItem(item);
            Destroy(gameObject);
        }
    }
}
