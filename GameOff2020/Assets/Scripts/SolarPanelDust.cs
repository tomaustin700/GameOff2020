﻿using UnityEngine;

public class SolarPanelDust : MonoBehaviour
{
    private float SolarDustTime;//150f
    public int solarPanelStage;
    [SerializeField] private Material stageOne;
    [SerializeField] private Material stageTwo;
    [SerializeField] private Material stageThree;
    [SerializeField] private Material stageFour;
    private new Renderer renderer;
    private PowerIO powerIO;
    // Start is called before the first frame update
    private void Awake()
    {
        powerIO = GetComponent<PowerIO>();
        renderer = GetComponentInChildren<Renderer>();
        powerIO.CurrentPowerProduction = powerIO.MaxPowerProduced;
        SolarDustTime = 150f;
    }
    void Start()
    {
        solarPanelStage = 0;
        SetMaterial();
        InvokeRepeating(nameof(IncreaseSolarPanelDust), SolarDustTime, SolarDustTime);
    }

    // Update is called once per frame
    void Update()
    {
    
        float powerMade = powerIO.IsDeviceOn ? (powerIO.MaxPowerProduced - solarPanelStage) : 0;
        powerIO.CurrentPowerProduction = powerMade;
    }
    public void SetMaterial()
    {
        switch (solarPanelStage)
        {
            case 0:
                renderer.material = stageOne;
                break;
            case 1:
                renderer.material = stageTwo;
                break;
            case 2:
                renderer.material = stageThree;
                break;
            case 3:
                renderer.material = stageFour;
                break;
            default:
                break;
        }
    }
    public void RemoveDust()
    {
        solarPanelStage = 0;
        SetMaterial();
    }
    void IncreaseSolarPanelDust()
    {
        if (solarPanelStage != 3)
        {
            solarPanelStage++;
            SetMaterial();
        }
    }
}
