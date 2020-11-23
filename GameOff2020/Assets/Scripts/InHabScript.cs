using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InHabScript : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    private PlayerManager playerManager;
    private void LateUpdate()
    {
        if (player == null)
        {
            player = GameObject.Find("Player_WithCamera");
        }
        playerManager = player.GetComponent<PlayerManager>();
    }
    void OnTriggerEnter()
    {
        playerManager.inHab = true;
    }
    void OnTriggerExit()
    {
        playerManager.inHab = false;
    }
}
