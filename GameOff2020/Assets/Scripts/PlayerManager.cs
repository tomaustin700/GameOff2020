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
    private Text tempText;
    private GameObject playerAstronaut;
    
    // Start is called before the first frame update
    void Start()
    {
        oxygenText = GameObject.Find("oxygenValue").GetComponent<Text>();
        healthText = GameObject.Find("healthValue").GetComponent<Text>();
        tempText = GameObject.Find("tempValue").GetComponent<Text>();
        playerAstronaut = GameObject.Find("Player_Astronaut");

        InvokeRepeating("UpdateOxygen", 0, 4.0f);
        InvokeRepeating("UpdateTemp", 0, 5.0f);

    }

    // Update is called once per frame
    void Update()
    {
        

    }

    void UpdateTemp()
    {
        var playerPos = playerAstronaut.transform;
        var temp = 100f;

        if (playerPos.position.x > playerPos.position.z)
        {
            temp = temp - (playerPos.position.x / 150);
        }
        else
        {
            temp = temp - (playerPos.position.z / 150);

        }

        var dec = decimal.Round((decimal)Random.Range(temp - 0.8f, temp + 0.8f), 1);

        tempText.text = dec.ToString() + "°c";

        if (dec > 150 || dec < -150)
        {
            tempText.color = Color.red;

            if (healthLevel - 25 >= 0)
                healthLevel = healthLevel - 25f;
            else
            {
                healthLevel = 0f;
            }
        }
        else
        {
            tempText.color = Color.black;
        }
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
