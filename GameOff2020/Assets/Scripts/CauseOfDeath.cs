using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauseOfDeath : MonoBehaviour
{
    public int cause;
    private void Start()
    {
        cause = 0;
        DontDestroyOnLoad(this);
    }
}
