using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IElement : IInventoryItem
{
    int chance { get; set; }
}
