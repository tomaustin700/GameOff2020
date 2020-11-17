using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Element : InventoryItem, IElement
{
    public new string name { get; set; }
    public int chance { get; set; }
    public Image image { get; set; }
}
