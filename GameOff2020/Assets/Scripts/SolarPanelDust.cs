using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarPanelDust : MonoBehaviour
{
    int solarPanelStage;
    [SerializeField] private Material stageOne;
    [SerializeField] private Material stageTwo;
    [SerializeField] private Material stageThree;
    [SerializeField] private Material stageFour;
    // Start is called before the first frame update
    void Start()
    {
        solarPanelStage = 1;
        InvokeRepeating(nameof(IncreaseSolarPanelDust), 0, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void IncreaseSolarPanelDust()
    {
        solarPanelStage++;
        switch (solarPanelStage)
        {
            case 2:
                GetComponent<Renderer>().material = stageTwo;
                break;
            case 3:
                GetComponent<Renderer>().material = stageThree;
                break;
            case 4:
                GetComponent<Renderer>().material = stageFour;
                break;
            default:
                break;
        }
    }
}
