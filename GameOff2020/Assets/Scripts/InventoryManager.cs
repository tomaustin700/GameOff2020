using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections.ObjectModel;
using UnityEngine.UI;
using UnityEditor;

public class InventoryManager : MonoBehaviour
{

    public int maxSize = 8;

    public List<InventoryItem> itemsInInventory;
    private GameObject hotbar;
    private int? selectedSlot;

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
            AddItem(new InventoryItem(Item.Aluminium));
        }

        if (hotbar != null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SelectHotbarSlot(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SelectHotbarSlot(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SelectHotbarSlot(3);

            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                SelectHotbarSlot(4);

            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                SelectHotbarSlot(5);

            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                SelectHotbarSlot(6);

            }
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                SelectHotbarSlot(7);

            }
            else if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                SelectHotbarSlot(8);

            }
            else if (Input.GetKeyDown(KeyCode.V))
            {
                DropItem();
            }
        }
    }

    void DropItem()
    {
        if (selectedSlot != null && itemsInInventory.ElementAtOrDefault(selectedSlot.Value -1) != null)
        {
            var item = itemsInInventory.ElementAtOrDefault(selectedSlot.Value - 1);
            var asset = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/" + item.name + ".prefab", typeof(Object)) as GameObject;
            var player = GameObject.Find("Player_Astronaut");
            Instantiate(asset, new Vector3(player.transform.position.x, 5, player.transform.position.z), Quaternion.identity);
            

        }
    }

    void SelectHotbarSlot(int slotToSelect)
    {
        if (itemsInInventory.ElementAtOrDefault(slotToSelect - 1) != null)
        {
            var slots = GetComponentsInChildren<RawImage>();

            foreach (var slot in slots)
            {
                slot.color = new Color(125f / 255f, 125f / 255f, 120f / 255f, 255f / 255f);
            }

            selectedSlot = slotToSelect;

            GetComponentsInChildren<RawImage>().Single(a => a.name == "Slot (" + slotToSelect + ")").color = Color.red;
        }
    }
}
