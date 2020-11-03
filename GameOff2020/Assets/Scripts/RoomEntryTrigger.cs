using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEntryTrigger : PlayerManager
{
    [SerializeField] private Material inHabMaterial;
    [SerializeField] private Material defaultMaterial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerExit()
    {
        inHab = !inHab;

        var wall1 = GameObject.Find("Wall 1").GetComponent<Renderer>();
        var wall2 = GameObject.Find("Wall 2").GetComponent<Renderer>();
        var wall3 = GameObject.Find("Wall 3").GetComponent<Renderer>();
        var wall4 = GameObject.Find("Wall 4").GetComponent<Renderer>();

        if (inHab)
        {
            wall1.material = inHabMaterial;
            wall2.material = inHabMaterial;
            wall3.material = inHabMaterial;
            wall4.material = inHabMaterial;
        }
        else
        {
            wall1.material = defaultMaterial;
            wall2.material = defaultMaterial;
            wall3.material = defaultMaterial;
            wall4.material = defaultMaterial;
        }
    }
}
