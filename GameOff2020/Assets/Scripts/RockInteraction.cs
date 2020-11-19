using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockInteraction : MonoBehaviour
{

    public void PickUpRock()
    {
        var formattedName = name.Replace("(Clone)", "");
        var item = new InventoryItem((Item)Enum.Parse(typeof(Item), formattedName, true));
        GameObject.FindGameObjectWithTag("Hotbar").GetComponent<InventoryManager>().AddItem(item);
        Destroy(gameObject);

    }


}
