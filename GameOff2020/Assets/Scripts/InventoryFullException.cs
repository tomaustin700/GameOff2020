using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryFullException : Exception
{
    public InventoryFullException()
    {
    }

    public InventoryFullException(string message)
        : base(message)
    {
    }

    public InventoryFullException(string message, Exception inner)
        : base(message, inner)
    {
    }

    protected InventoryFullException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
    {
        throw new NotImplementedException();
    }
}
