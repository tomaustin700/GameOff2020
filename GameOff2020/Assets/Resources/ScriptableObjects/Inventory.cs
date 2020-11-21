using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Custom/Inventory")]
public class Inventory : ScriptableObject
{
    public InventoryItem[,] InventorySlots = {};

    public void AddItem(InventoryItem item)
    {
        for (int k = 0; k < InventorySlots.GetLength(0); k++)
        {
            for (int l = 0; l < InventorySlots.GetLength(1); l++)
            {
               if(InventorySlots[k,l] == null)
                {
                    item.posX = k;
                    item.posY = l;
                    InventorySlots[k,l] = item;
                    return;
                }
            }
        }
    }
    public bool CanAddItem()
    {
        for (int k = 0; k < InventorySlots.GetLength(0); k++)
        {
            for (int l = 0; l < InventorySlots.GetLength(1); l++)
            {
                if (InventorySlots[k, l] == null)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public void AddItemToSlot(InventoryItem item,int x, int y)
    {
        if(InventorySlots[x,y] == null)
        {
            item.posX = x;
            item.posY = y;
            InventorySlots[x,y] = item;
        }
    }
    public void RemoveItemFromSlot(int x, int y)
    {
        if (InventorySlots[x,y] != null)
        {
            InventorySlots[x,y] = null;
        }
    }

    public InventoryItem GetItemByGuid(string guid)
    {
        for (int k = 0; k < InventorySlots.GetLength(0); k++)
        {
            for (int l = 0; l < InventorySlots.GetLength(1); l++)
            {
                var item = InventorySlots[k, l];
                if (item != null && item.refId.ToString() == guid)
                {

                    return InventorySlots[k, l];
                }
            }
        }
        return null;
    }
}
