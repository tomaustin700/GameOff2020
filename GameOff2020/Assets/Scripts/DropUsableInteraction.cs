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
        GameObject.FindGameObjectWithTag("Hotbar").GetComponent<InventoryManager>().AddItem(item);
        Destroy(gameObject);

    }
}
