using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private Healthbar suitTempLvl;
    private Healthbar foodLvl;
    private Text tempText;
    private GameObject playerAstronaut;
    private float temp;
    [SerializeField] public GameObject causeOfDeath;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        yield return new WaitForSeconds(0.5f);

        oxygenLvl = GameObject.Find("oxygenValue").GetComponent<Healthbar>();
        healthLvl = GameObject.Find("healthValue").GetComponent<Healthbar>();
        powerLvl = GameObject.Find("powerValue").GetComponent<Healthbar>();
        suitTempLvl = GameObject.Find("suitTempValue").GetComponent<Healthbar>();
        foodLvl = GameObject.Find("hungerValue").GetComponent<Healthbar>();

        tempText = GameObject.Find("tempValue").GetComponent<Text>();
        playerAstronaut = GameObject.Find("Player_Astronaut");

        InvokeRepeating(nameof(UpdateOxygen), 4, 4.0f);
        InvokeRepeating(nameof(UpdatePower), 60, 180);
        InvokeRepeating(nameof(UpdateTemp), 0, 5.0f);
        InvokeRepeating(nameof(UpdateSuitTemp), 0, 5.0f);
        InvokeRepeating(nameof(UpdateFood), 120, 120);

        oxygenLvl.SetHealth(100);
        healthLvl.SetHealth(100);
        powerLvl.SetHealth(100);
        foodLvl.SetHealth(100);
        temp = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthLvl != null && healthLvl.health <= 0)
            playerDead = true;

        if (playerDead)
        {
            if (suitTempLvl.health == 0)
            {
                causeOfDeath.GetComponent<CauseOfDeath>().cause = 1;
            }
            else if (oxygenLvl.health == 0)
            {
                causeOfDeath.GetComponent<CauseOfDeath>().cause = 2;
            }
            else
            {
                //testing
                causeOfDeath.GetComponent<CauseOfDeath>().cause = 0;
            }
            PlayerDead();
        }

        if (FindObjectOfType<GameManager>().daysUntilRescue == 0)
            SceneManager.LoadScene(3, LoadSceneMode.Single);



        if (Application.isEditor && Input.GetKeyUp(KeyCode.Backspace))
            playerDead = true;

        if (Application.isEditor && Input.GetKeyUp(KeyCode.Return))
            FindObjectOfType<GameManager>().daysUntilRescue = 0;
    }

    void UpdateFood()
    {
        if (foodLvl.health > 0)
            foodLvl.SetHealth(foodLvl.health - 10);

        if (foodLvl.health <= 0)
        {
            healthLvl.SetHealth(healthLvl.health - 25f);
        }
    }

    public void AddFood(float amount)
    {
        foodLvl.SetHealth(foodLvl.health + amount);
    }

    void UpdateSuitTemp()
    {
        if (powerLvl.health > 0)
        {
            suitTempLvl.health = Random.Range(35 - 2f, 35 + 0.8f);
        }

        if (suitTempLvl.health == 0 && healthLvl.health - 25 >= 0)
        {
            if (healthLvl.health - 25 >= 0)
                healthLvl.SetHealth(healthLvl.health - 25f);
            else
            {
                healthLvl.SetHealth(0f);
            }
        }
    }

    void UpdateTemp()
    {
        var playerPos = playerAstronaut.transform;
        temp = -100f;

        if (playerPos.position.x > playerPos.position.z)
        {
            temp = temp - (playerPos.position.x / 20);
        }
        else
        {
            temp = temp - (playerPos.position.z / 20);

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

    public void AddOxygen(int amount)
    {
        oxygenLvl.SetHealth(oxygenLvl.health + amount);
    }

    void UpdatePower()
    {

        var tempMultiplier = (temp > 0 ? temp : (temp * -1)) * 0.05;
        powerLvl.SetHealth(powerLvl.health - powerSubtractionMultiplier - (float)tempMultiplier);


        if (powerLvl.health == 0 && suitTempLvl.health > 0)
        {
            if (suitTempLvl.health - 25 >= 0)
                suitTempLvl.SetHealth(suitTempLvl.health - 25f);
            else
            {
                suitTempLvl.SetHealth(0f);
            }
        }
        else if (powerLvl.health > 0 && suitTempLvl.health < 20)
        {
            suitTempLvl.health = 37;
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


            if (oxygenLvl.health <= 0 && healthLvl.health > 0)
            {
                if (healthLvl.health - 50 >= 0)
                    healthLvl.SetHealth(healthLvl.health - 50f);
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
    public void PlayerDead()
    {
        SceneManager.LoadScene("PlayerDeadScene", LoadSceneMode.Single);
    }
}
