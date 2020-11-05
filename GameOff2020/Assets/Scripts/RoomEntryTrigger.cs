using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEntryTrigger : MonoBehaviour
{
    [SerializeField] private Material inHabMaterial;
    [SerializeField] private Material defaultMaterial;
    // Start is called before the first frame update

    private PlayerManager playerManager;

    GameObject wall1;
    GameObject wall2;
    GameObject wall3;
    GameObject wall4;

    private void Awake()
    {
        playerManager = GameObject.Find("Player_WithCamera").GetComponent<PlayerManager>();
    }

    void Start()
    {
        wall1 = GameObject.Find("Wall 1");
        wall2 = GameObject.Find("Wall 2");
        wall3 = GameObject.Find("Wall 3");
        wall4 = GameObject.Find("Wall 4");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerExit()
    {
        playerManager.inHab = false;
        wall1 = GameObject.Find("Wall 1");
        wall2 = GameObject.Find("Wall 2");
        wall3 = GameObject.Find("Wall 3");
        wall4 = GameObject.Find("Wall 4");
        wall1.GetComponent<Renderer>().material = defaultMaterial;
        wall2.GetComponent<Renderer>().material = defaultMaterial;
        wall3.GetComponent<Renderer>().material = defaultMaterial;
        wall4.GetComponent<Renderer>().material = defaultMaterial;
    }
    void OnTriggerEnter()
    {
        playerManager.inHab = true;
        wall1 = GameObject.Find("Wall 1");
        wall2 = GameObject.Find("Wall 2");
        wall3 = GameObject.Find("Wall 3");
        wall4 = GameObject.Find("Wall 4");
        wall1.GetComponent<Renderer>().material = inHabMaterial;
        wall2.GetComponent<Renderer>().material = inHabMaterial;
        wall3.GetComponent<Renderer>().material = inHabMaterial;
        wall4.GetComponent<Renderer>().material = inHabMaterial;
    }
}
