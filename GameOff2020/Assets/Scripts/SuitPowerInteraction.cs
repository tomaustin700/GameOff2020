using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuitPowerInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerManager playerManager;
    public void RechargeSuit()
    {
        if (playerManager == null)
            playerManager = FindObjectOfType<PlayerManager>();

        playerManager.Recharge();

    }
}
