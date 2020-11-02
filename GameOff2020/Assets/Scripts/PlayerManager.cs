using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float oxygenLevel = 100f;
    public Text oxygenText;
    public bool inHab;
    public bool playerDead;
    public float oxygenSubtractionMultiplier = 1;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateOxygen", 0, 2.0f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateOxygen()
    {
        if (!inHab)
        {
            oxygenLevel = oxygenLevel - oxygenSubtractionMultiplier;
            if (oxygenLevel < 20)
                oxygenText.color = Color.red;
            else
                oxygenText.color = Color.black;

            if (oxygenLevel == 0)
                playerDead = true;
        }
        else
        {
            if (oxygenLevel < 100)
                oxygenLevel = 100;
        }

        oxygenText.text = "Oxygen: " + oxygenLevel;

    }
}
