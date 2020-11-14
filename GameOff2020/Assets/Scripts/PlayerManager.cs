using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{

    public bool inHab;
    public bool playerDead;
    public float oxygenSubtractionMultiplier = 1;
    public float powerSubtractionMultiplier = 1;

    private Healthbar oxygenLvl;
    private Healthbar healthLvl;
    private Healthbar powerLvl;
    private Text tempText;
    private GameObject playerAstronaut;
    private float temp;

    // Start is called before the first frame update
    void Start()
    {
        oxygenLvl = GameObject.Find("oxygenValue").GetComponent<Healthbar>();
        healthLvl = GameObject.Find("healthValue").GetComponent<Healthbar>();
        powerLvl = GameObject.Find("powerValue").GetComponent<Healthbar>();
        tempText = GameObject.Find("tempValue").GetComponent<Text>();
        playerAstronaut = GameObject.Find("Player_Astronaut");

        oxygenLvl.SetHealth(100);
        healthLvl.SetHealth(100);
        powerLvl.SetHealth(100);
        temp = 100f;

        InvokeRepeating("UpdateOxygen", 4, 4.0f);
        InvokeRepeating("UpdatePower", 60, 180);
        InvokeRepeating("UpdateTemp", 0, 5.0f);

    }

    // Update is called once per frame
    void Update()
    {


    }

    void UpdateTemp()
    {
        var playerPos = playerAstronaut.transform;
        temp = 100f;

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
            //tempText.color = Color.red;

            if (healthLvl.health - 25 >= 0)
                healthLvl.SetHealth(healthLvl.health - 25f);
            else
            {
                healthLvl.SetHealth(0f);
            }
        }
        else
        {
            // tempText.color = Color.black;
        }
    }

    void UpdatePower()
    {

        var tempMultiplier = (temp > 0 ? temp : (temp * -1)) * 0.05;
        powerLvl.SetHealth(powerLvl.health - powerSubtractionMultiplier - (float)tempMultiplier);


        if (powerLvl.health == 0 && healthLvl.health > 0)
        {
            if (healthLvl.health - 25 >= 0)
                healthLvl.SetHealth(healthLvl.health - 25f);
            else
            {
                healthLvl.SetHealth(0f);
            }
        }

    }

    public void Recharge()
    {
        powerLvl.SetHealth(100);
    }

    void UpdateOxygen()
    {
        if (!inHab)
        {
            oxygenLvl.SetHealth(oxygenLvl.health - oxygenSubtractionMultiplier);


            if (oxygenLvl.health == 0 && healthLvl.health > 0)
            {
                if (healthLvl.health - 25 >= 0)
                    healthLvl.SetHealth(healthLvl.health - 25f);
                else
                {
                    healthLvl.SetHealth(0f);
                }
            }
        }
        else
        {
            if (oxygenLvl.health < 100)
                oxygenLvl.SetHealth(100);
        }


    }
}
