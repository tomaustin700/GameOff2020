using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotScript : MonoBehaviour
{
    private void Awake()
    {
        Owner = GameObject.FindGameObjectWithTag("InventoryUI").GetComponent<CurrentInventoryOwner>();
    }
    public CurrentInventoryOwner Owner;
    public void AddToCurrentInventory()
    {
        Owner.TransferToHotBar(gameObject);
    }
}
