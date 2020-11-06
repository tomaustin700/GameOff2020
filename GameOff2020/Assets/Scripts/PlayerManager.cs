using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float oxygenLevel = 100f;
    public float healthLevel = 100f;
    public bool inHab;
    public bool playerDead;
    public float oxygenSubtractionMultiplier = 1;

    private Text oxygenText;
    private Text healthText;
    // Start is called before the first frame update
    void Start()
    {
        oxygenText = GameObject.Find("oxygenValue").GetComponent<Text>();
        healthText = GameObject.Find("healthValue").GetComponent<Text>();
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

            if (oxygenLevel == 0 && healthLevel > 0)
            {
                if (healthLevel - 25 >= 0)
                    healthLevel = healthLevel - 25f;
                else
                {
                    healthLevel = 0f;
                }
            }
        }
        else
        {
            if (oxygenLevel < 100)
                oxygenLevel = 100;
        }

        oxygenText.text = oxygenLevel.ToString();

    }
}
