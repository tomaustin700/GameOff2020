using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : InventoryItem, IElement
{
    public new string name { get; set; }
    public int chance { get; set; }
}
