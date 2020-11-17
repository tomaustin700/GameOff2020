using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections.ObjectModel;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{

    public int maxSize = 8;

    public List<InventoryItem> itemsInInventory;
    private GameObject hotbar;

    private void Awake()
    {
        if (tag != "UseableItem")
            hotbar = GameObject.FindGameObjectWithTag("Hotbar");
    }
    void Start()
    {
        itemsInInventory = new List<InventoryItem>();
    }

    public void AddItem(InventoryItem item)
    {
        if (itemsInInventory.Count < maxSize)
        {

            itemsInInventory.Add(item);
            if (hotbar != null)
            {
                var hotbatSlotToFill = "Slot (" + itemsInInventory.Count + ")";

                var slot = GetComponentsInChildren<RawImage>().Single(a => a.name == hotbatSlotToFill);
                slot.texture = item.sprite;
            }

        }

    }


    public void RemoveItem(IInventoryItem item)
    {
        itemsInInventory.Remove(itemsInInventory.Single(a => a.name == item.name));

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            AddItem(new InventoryItem("Aluminium"));
        }
    }
}
