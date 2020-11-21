using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CurrentInventoryOwner : MonoBehaviour
{
    public GameObject CurrentOwner = null;
    public GameObject InventorySlotForReplace;

    public void TransferToHotBar(GameObject gameObject)
    {
        if(CurrentOwner != null)
        {
            var hotbar = GameObject.FindGameObjectWithTag("Hotbar").GetComponent<InventoryManager>();
            if(hotbar.HasInventorySpace())
            {
                var item = CurrentOwner.GetComponent<Storage>().Inventory.GetItemByGuid(gameObject.name);
                if(item != null)
                {
                    hotbar.AddItem(item);
                    CurrentOwner.GetComponent<Storage>().Inventory.RemoveItemFromSlot(item.posX,item.posY);
                    CurrentOwner.GetComponent<Storage>().ReDraw();
                    Destroy(gameObject);
                }
            }
           
        }
    }
    public void TransferToCurrentInventory(int slot)
    {
        if (CurrentOwner != null)
        {
            var manager = GameObject.FindGameObjectWithTag("Hotbar").GetComponent<InventoryManager>();
            var storage = CurrentOwner.GetComponent<Storage>();
            if(storage.Locked)
            {
                return;
            }
            var inv = storage.Inventory;
            if (inv.CanAddItem())
            {
                var exists = manager.hotbarLocations.Any(a => a.hotbarLocation.Contains(slot.ToString()));
                if(exists)
                {
                    var slotItem = manager.hotbarLocations.First(a => a.hotbarLocation.Contains(slot.ToString()));
                    var item = manager.itemsInInventory.First(x => x.refId == slotItem.itemGuid);
                    inv.AddItem(item);
                    storage.ReDraw();
                    manager.DropItemBySlot(slotItem,false);
                }
                
              
            }
        }
    }
    
}
