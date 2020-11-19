using UnityEngine;

public class SolarPanelDust : MonoBehaviour
{
    [SerializeField] private float SolarDustTime;//150f
    private int solarPanelStage;
    [SerializeField] private Material stageOne;
    [SerializeField] private Material stageTwo;
    [SerializeField] private Material stageThree;
    [SerializeField] private Material stageFour;
    private new Renderer renderer;
    private PowerIO powerIO;
    // Start is called before the first frame update
    private void Awake()
    {
        //powerIO = GetComponent<PowerIO>();
        renderer = GetComponentInChildren<Renderer>();
        //powerIO.CurrentPowerProduction = powerIO.MaxPowerProduced;
        
    }
    void Start()
    {
        solarPanelStage = 0;
        InvokeRepeating(nameof(IncreaseSolarPanelDust), SolarDustTime, SolarDustTime);
    }

    // Update is called once per frame
    void Update()
    {
        //float powerMade = powerIO.IsDeviceOn ? (powerIO.MaxPowerProduced - solarPanelStage) : 0;
        //Debug.Log($"{powerIO.MaxPowerProduced} - {solarPanelStage} = {powerMade}");
        //powerIO.CurrentPowerProduction = powerMade;

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
    }
    void IncreaseSolarPanelDust()
    {
        if (solarPanelStage != 3)
        {
            solarPanelStage++;
        }
    }
}
