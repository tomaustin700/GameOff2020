using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCauseOfDeath : MonoBehaviour
{
    GameObject CauseOfDeathText;
    void Start()
    {
        CauseOfDeath cod = FindObjectOfType<CauseOfDeath>();
        switch (cod.cause)
        {
            case 1:
                //Hypothermia
                CauseOfDeathText = GameObject.Find("CauseOfDeathText");
                CauseOfDeathText.GetComponent<TextMeshProUGUI>().text = "Hypothermia";
                GameObject.Find("HypothermiaTip").GetComponent<TextMeshProUGUI>().enabled = true;
                break;
            case 2:
                //Asphyxia
                CauseOfDeathText = GameObject.Find("CauseOfDeathText");
                CauseOfDeathText.GetComponent<TextMeshProUGUI>().text = "Asphyxia";
                GameObject.Find("AsphyxiaTip").GetComponent<TextMeshProUGUI>().enabled = true;
                break;
            case 3:
                //Asphyxia
                CauseOfDeathText = GameObject.Find("CauseOfDeathText");
                CauseOfDeathText.GetComponent<TextMeshProUGUI>().text = "Starvation";
                GameObject.Find("StarvationTip").GetComponent<TextMeshProUGUI>().enabled = true;
                break;
            default:
                //Testing
                CauseOfDeathText = GameObject.Find("CauseOfDeathText");
                CauseOfDeathText.GetComponent<TextMeshProUGUI>().text = "Testing";
                GameObject.Find("TestingTip").GetComponent<TextMeshProUGUI>().enabled = true;
                break;
        }
    }
}
