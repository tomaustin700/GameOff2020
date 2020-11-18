using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    public string name { get; set; }
    public Texture sprite { get; set; }

    public InventoryItem(Item item)
    {
        name = item.ToString();
        sprite = Resources.Load<Texture>("Sprites/" + name);

    }

}
