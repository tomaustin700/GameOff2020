using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InHabScript : MonoBehaviour
{
    // Start is called before the first frame update

    private PlayerManager playerManager;
    private void LateUpdate()
    {
        if (playerManager == null)
        {
            playerManager = GameObject.Find("Player_WithCamera(Clone)").GetComponent<PlayerManager>();
        }
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
