using System;
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
            RecalculateAllPositions();
        }
    }
    public void RecalculateAllPositions()
    {
        for (int k = 0; k < InventorySlots.GetLength(0); k++)
        {
            for (int l = 0; l < InventorySlots.GetLength(1); l++)
            { 
                if (InventorySlots[k, l] != null)
                {
                    InventorySlots[k, l].posX = k;
                    InventorySlots[k, l].posY = l;
                }
            }
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
    public bool Contains(Item item_type)
    {
        for (int k = 0; k < InventorySlots.GetLength(0); k++)
        {
            for (int l = 0; l < InventorySlots.GetLength(1); l++)
            {
                var item = InventorySlots[k, l];
                if (item != null && item.name == item_type.ToString())
                {

                    return true;
                }
            }
        }
        return false;
    }
   
    public InventoryItem GetItemByItemType(Item item_type)
    {
        RecalculateAllPositions();
        for (int k = 0; k < InventorySlots.GetLength(0); k++)
        {
            for (int l = 0; l < InventorySlots.GetLength(1); l++)
            {
                var item = InventorySlots[k, l];
                if (item != null && item.name == item_type.ToString())
                {

                    return item;
                }
            }
        }
        return null;
    }
    public List<Item> GetAllItemsAsItemTypes()
    {
        List<Item> returnItems = new List<Item>();
        for (int k = 0; k < InventorySlots.GetLength(0); k++)
        {
            for (int l = 0; l < InventorySlots.GetLength(1); l++)
            {
                var item = InventorySlots[k, l];
                if (item != null)
                {
                    var type = Enum.Parse(typeof(Item), item.name);
                    if (type != null && type is Item)
                    {
                        returnItems.Add((Item)type);
                    }
                    
                }
            }
        }
        return returnItems;
    }
    public List<string> GetAllItemNames()
    {
        List<string> returnItems = new List<string>();
        for (int k = 0; k < InventorySlots.GetLength(0); k++)
        {
            for (int l = 0; l < InventorySlots.GetLength(1); l++)
            {
                var item = InventorySlots[k, l];
                if (item != null)
                {
                    returnItems.Add(item.name);

                }
            }
        }
        return returnItems;
    }
    public List<InventoryItem> GetAllItemsAsInventoryItems()
    {
        List<InventoryItem> returnItems = new List<InventoryItem>();
        for (int k = 0; k < InventorySlots.GetLength(0); k++)
        {
            for (int l = 0; l < InventorySlots.GetLength(1); l++)
            {
                var item = InventorySlots[k, l];
                if (item != null)
                {
                    returnItems.Add(item);

                }
            }
        }
        return returnItems;
    }
}
