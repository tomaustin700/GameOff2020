using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections.ObjectModel;

public class InventoryManager : MonoBehaviour
{

    public int maxSize = 8;

    public ObservableCollection<IInventoryItem> itemsInInventory;


    void Start()
    {
        itemsInInventory = new ObservableCollection<IInventoryItem>();
    }

    public void AddItem(IInventoryItem item)
    {
        if (itemsInInventory.Count < maxSize)
        {

            itemsInInventory.Add(item);

        }
        else
            throw new InventoryFullException();
    }


    public void RemoveItem(IInventoryItem item)
    {
        itemsInInventory.Remove(itemsInInventory.Single(a => a.name == item.name));

    }
}
