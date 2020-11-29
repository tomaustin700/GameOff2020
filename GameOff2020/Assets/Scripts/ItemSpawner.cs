using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    public GameObject commonRockPrefab;
    public GameObject rareRockPrefab;


    public float commonRockXSpread = 300;
    public float commonRockYSpread = 2;
    public float commonRockZSpread = 300;
    public int commonRocksToSpawn = 50;

    public float rareRockXSpread = 300;
    public float rareRockYSpread = 2;
    public float rareRockZSpread = 300;
    public int rareRocksToSpawn = 10;

    public void Spawn()
    {
        for (int i = 0; i < commonRocksToSpawn; i++)
        {
            SpreadCommonRocks();
        }

        for (int i = 0; i < rareRocksToSpawn; i++)
        {
            SpreadRareRocks();
        }

    }

    void SpreadCommonRocks()
    {
        Vector3 randPosition = new Vector3(Random.Range(-commonRockXSpread, commonRockXSpread),
            Random.Range(-commonRockYSpread, commonRockYSpread), Random.Range(-commonRockZSpread, commonRockZSpread)) + transform.position;
        var inst = Instantiate(commonRockPrefab, randPosition, Quaternion.identity);
        var rock = inst.GetComponent<RockSelector>().Pick();
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(inst.transform.position, -inst.transform.up, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Default")))
        {
            var rockComp = rock.GetComponentInChildren<Rigidbody>();
            rockComp.velocity = Vector3.zero;
            rockComp.angularDrag = 10;
            rockComp.gameObject.transform.position = hit.point + (Vector3.up * 0.5f);

        }
        Destroy(inst);
    }

    void SpreadRareRocks()
    {
        Vector3 randPosition = new Vector3(Random.Range(-rareRockXSpread, rareRockXSpread),
            Random.Range(-rareRockYSpread, rareRockYSpread), Random.Range(-rareRockZSpread, rareRockZSpread)) + transform.position;
        var inst = Instantiate(rareRockPrefab, randPosition, Quaternion.identity);
        var rock = inst.GetComponent<RockSelector>().Pick();
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(inst.transform.position, -inst.transform.up, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Default")))
        {
            var rockComp = rock.GetComponentInChildren<Rigidbody>();
            rockComp.velocity = Vector3.zero;
            rockComp.angularDrag = 10;
            rockComp.gameObject.transform.position = hit.point + (Vector3.up * 0.5f);
            
        }
        Destroy(inst);
    }
}
