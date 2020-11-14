using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventoryManager : MonoBehaviour
{

    public int maxSize = 8;

    public List<InventoryItem> itemsInInventory;


    void Start()
    {
        itemsInInventory = new List<InventoryItem>();
    }

    public void AddItem(InventoryItem item)
    {
        if (itemsInInventory.Count <= maxSize)
        {
            if (itemsInInventory.Any(x => x.name == item.name))
            {
                var inventItem = itemsInInventory.First(x => x.name == item.name);
                inventItem.count = inventItem.count + item.count;
            }
            else
            {
                itemsInInventory.Add(item);
            }
        }
        else
            throw new InventoryFullException();
    }

    public void UpdateCount(InventoryItem item, int countChange)
    {
        var inventItem = itemsInInventory.First(x => x.name == item.name);
        inventItem.count = inventItem.count + countChange;

        if (inventItem.count <= 0)
            itemsInInventory.Remove(inventItem);
    }

    public void RemoveItem(InventoryItem item)
    {
        itemsInInventory.Remove(itemsInInventory.Single(a => a.name == item.name));

    }
}
