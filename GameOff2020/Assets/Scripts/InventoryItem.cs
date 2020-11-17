using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    public string name { get; set; }
    public Texture sprite { get; set; }

    public InventoryItem(string name)
    {
        this.name = name;
        sprite = Resources.Load<Texture>("Sprites/" + name);

    }

}
