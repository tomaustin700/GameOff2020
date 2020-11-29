using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSelector : MonoBehaviour
{

    public GameObject[] potentialRocks;




   public GameObject Pick()
    {
        int randomIndex = Random.Range(0, potentialRocks.Length);
        return Instantiate(potentialRocks[randomIndex], transform.position, Quaternion.identity);
    }


}
