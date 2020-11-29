using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RockGrinder : MonoBehaviour
{
    private System.Random rnd;

    void Start()
    {
        rnd = new System.Random();
    }
    InventoryItem SelectItem(List<(InventoryItem, int chance)> items)
    {
        int poolSize = 0;
        for (int i = 0; i < items.Count; i++)
        {
            poolSize += items[i].chance;
        }

        int randomNumber = rnd.Next(0, poolSize) + 1;

        int accumulatedProbability = 0;
        for (int i = 0; i < items.Count; i++)
        {
            accumulatedProbability += items[i].chance;
            if (randomNumber <= accumulatedProbability)
                return items[i].Item1;
        }
        return null;
    }


    public void TradeRock()
    {
        List<(InventoryItem, int chance)> elements = new List<(InventoryItem, int chance)>();
        var manager = GameObject.FindGameObjectWithTag("Hotbar").GetComponent<InventoryManager>();
        var exists = manager.hotbarLocations.Any(a => a.hotbarLocation.Contains(manager.SelectedSlot.ToString()));
        if (exists)
        {
            var slotItem = manager.hotbarLocations.First(a => a.hotbarLocation.Contains(manager.SelectedSlot.ToString()));
            var item = manager.itemsInInventory.First(x => x.refId == slotItem.itemGuid);

            if (item.name == nameof(Item.Rock1) || item.name == nameof(Item.Rock2) || item.name == nameof(Item.Rock3))
            {
                elements.Add((new InventoryItem(Item.Silicon), 45));
                elements.Add((new InventoryItem(Item.Aluminium), 20));
                elements.Add((new InventoryItem(Item.Iron), 20));
            }
            else if (item.name == nameof(Item.Rock4) || item.name == nameof(Item.Rock5))
            {
                elements.Add((new InventoryItem(Item.Magnesium), 10));
                elements.Add((new InventoryItem(Item.Titanium), 10));
                elements.Add((new InventoryItem(Item.Iron), 10));
            }
            else
            {
                return;
            }
            manager.DropItemBySlot(slotItem, false);
            manager.AddItem(SelectItem(elements));
        }
        else
        {
            return;
        }
    }
}
