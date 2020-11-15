using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerManagement : MonoBehaviour
{
    public float MaxPower;
    public float CurrentPowerProduction;
    public float CurrentPowerUsage;
    public ObservableCollection<PowerIO> RequiresPower = new ObservableCollection<PowerIO>();
    public ObservableCollection<PowerIO> PowerProduced = new ObservableCollection<PowerIO>();
    public GameObject PowerUI;
    // Start is called before the first frame update
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var text = PowerUI.GetComponent<Text>();
        MaxPower = PowerProduced.Sum(x => x.MaxPowerProduced);
        CurrentPowerProduction = PowerProduced.Sum(x => x.CurrentPowerProduction);
        CurrentPowerUsage = RequiresPower.Where(x => x.IsDeviceOn).Sum(x => x.CurrentPowerConsumption);
        var requestingPower = RequiresPower.Where(x => x.IsDeviceOn).Sum(x => x.MaxRequiredPower);
        if(requestingPower > CurrentPowerProduction)
        {
            text.color = Color.red;
        }
        else
        {
            text.color = Color.white;
        }
        text.text = $"Power: {requestingPower}/{CurrentPowerProduction} ({MaxPower})";
    }
    public bool RequestPower(float amount, float currentPowerConsumption)
    {
        var availablePower = CurrentPowerProduction - CurrentPowerUsage;
        var potentialPowerAfterUsage = availablePower - amount;
        if(potentialPowerAfterUsage + currentPowerConsumption >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
