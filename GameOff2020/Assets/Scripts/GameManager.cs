using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int daysSurvived = 0;

    private ItemSpawner itemSpawner;


    void Start()
    {
        InvokeRepeating("UpdateDaysSurvived", 0, 600);
        SpawnItems();
        Physics.gravity = new Vector3(0, -3, 0);

    }

    void SpawnItems()
    {
        itemSpawner = GetComponent<ItemSpawner>();
        if (itemSpawner != null)
            itemSpawner.Spawn();
    }


    // Update is called once per frame
    void UpdateDaysSurvived()
    {
        daysSurvived++;
    }




}
