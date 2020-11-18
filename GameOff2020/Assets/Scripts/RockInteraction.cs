using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockInteraction : MonoBehaviour
{
    private bool isRare;

    public void PickUpRock()
    {

        if (gameObject.name == "Rock4" || gameObject.name == "Rock5")
        {
            isRare = true;
        }

        GameObject.FindGameObjectWithTag("Hotbar").GetComponent<InventoryManager>().AddItem(isRare ? new InventoryItem("RareRock") : new InventoryItem("CommonRock"));
        Destroy(gameObject);

    }


}
