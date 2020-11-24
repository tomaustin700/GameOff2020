using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    public string name { get; set; }
    public Texture sprite { get; set; }
    public Guid refId { get; set; }

    public bool Placeable
    {
        get
        {
            switch (name)
            {
                case "Drill":
                    return true;
                case "Printer":
                    return true;
                case "RockGrinder":
                    return true;
                case "Locker":
                    return true;
                case "CommunicationsDevice":
                    return true;
                case "Solar":
                    return true;
                default:
                    return false;
            }
        }
    }

    public int posX;
    public int posY;
    public InventoryItem(Item item)
    {
        name = item.ToString();
        refId = Guid.NewGuid();
        sprite = Resources.Load<Texture>("Sprites/" + name);

    }

}
