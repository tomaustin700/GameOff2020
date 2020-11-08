using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSelector : MonoBehaviour
{

    public GameObject[] potentialRocks;

    void Start()
    {
        Pick();
    }

    void Pick()
    {
        int randomIndex = Random.Range(0, potentialRocks.Length);
        Instantiate(potentialRocks[randomIndex], transform.position, Quaternion.identity);
    }


}
