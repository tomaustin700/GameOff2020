using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IInventoryItem
{

    string name { get; set; }
    Sprite sprite { get; set; }
}
