using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int daysUntilRescue = 100;

    private ItemSpawner itemSpawner;


    void Start()
    {
        InvokeRepeating("UpdateDaysUntilRescue", 0, 600);
        SpawnItems();

        Physics.gravity = new Vector3(0, -3, 0);


    }

    private void Update()
    {
       

    }

    void SpawnItems()
    {
        itemSpawner = GetComponent<ItemSpawner>();
        if (itemSpawner != null)
            itemSpawner.Spawn();
    }


    // Update is called once per frame
    void UpdateDaysUntilRescue()
    {
        if (daysUntilRescue > 0)
            daysUntilRescue--;
    }




}
