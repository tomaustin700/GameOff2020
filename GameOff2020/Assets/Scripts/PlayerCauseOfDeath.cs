using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCauseOfDeath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerManager player = FindObjectOfType<PlayerManager>();

        switch (player.causeOfDeath)
        {
            case 1:
                //Hypothermia
                GameObject.Find("CauseOfDeathText").GetComponent<TextMeshPro>().text = "Hypothermia";
                GameObject.Find("HypothermiaTip").SetActive(true);
                break;
            case 2:
                //Asphyxia
                GameObject.Find("CauseOfDeathText").GetComponent<TextMeshPro>().text = "Asphyxia";
                GameObject.Find("AsphyxiaTip").SetActive(true);
                break;
            default:
                //Testing
                GameObject.Find("CauseOfDeathText").GetComponent<TextMeshPro>().text = "Testing";
                GameObject.Find("TestingTip").SetActive(true);
                break;
        }
    }
}
