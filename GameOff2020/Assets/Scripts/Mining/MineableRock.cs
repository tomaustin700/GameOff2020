using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is a base class that mineable rocks can inherit from
public class MineableRock : MonoBehaviour
{
    public int yeildAmount;
    public bool canMine
    {
        get
        {
            return yeildAmount > 0;
        }
    }

    public void Mine()
    {
        if (yeildAmount > 0)
            yeildAmount = yeildAmount - 10;
    }
}
