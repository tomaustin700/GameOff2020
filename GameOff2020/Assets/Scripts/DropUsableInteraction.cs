using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropUsableInteraction : MonoBehaviour
{
    public void PickUpUsable()
    {
        var formattedName = name.Replace("Drop(Clone)", "");
        var item = new InventoryItem((Item)Enum.Parse(typeof(Item), formattedName, true));
        var hotbar = GameObject.FindGameObjectWithTag("Hotbar").GetComponent<InventoryManager>();
        if (hotbar.HasInventorySpace())
        {
            GameObject.FindGameObjectWithTag("Hotbar").GetComponent<InventoryManager>().AddItem(item);
            Destroy(gameObject);
        }
    }
}
