using System.Collections;
using System.Collections.Generic;
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
                   // Instantiate(InventorySlotForReplace, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform.parent);
                    Destroy(gameObject);
                }
            }
           
        }
    }
}
