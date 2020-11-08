using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject rockPrefab;


    public float rockXSpread = 10;
    public float rockYSpread = 2;
    public float rockZSpread = 10;
    public int rocksToSpawn = 10;

    public void Spawn()
    {
        //Spawn player - use similar logic to spawn other items that player starts with such as hab
        Instantiate(playerPrefab, new Vector3(0, 2, 0), Quaternion.identity);


        for (int i = 0; i < rocksToSpawn; i++)
        {
            SpreadRocks();
        }
    }

    void SpreadRocks()
    {
        Vector3 randPosition = new Vector3(Random.Range(-rockXSpread, rockXSpread), Random.Range(-rockYSpread, rockYSpread), Random.Range(-rockZSpread, rockZSpread)) + transform.position;
        Instantiate(rockPrefab, randPosition, Quaternion.identity);

    }
}
