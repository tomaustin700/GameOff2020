using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class PowerIO : MonoBehaviour, INotifyPropertyChanged
{
    public float MaxPowerProduced;
    public float MaxRequiredPower;
    public PowerManagement PowerManager = null;
    public bool Destroying = false;
    private bool _isDeviceOn = true;

    public event PropertyChangedEventHandler PropertyChanged;
    [SerializeField]
    private float _currentPowerProduction;

    public float CurrentPowerProduction
    {
        get { return _currentPowerProduction; }
        set { 
            if (_currentPowerProduction != value)
            {
                _currentPowerProduction = value;
                NotifyPropertyChanged();
            }
        }
    }

    [SerializeField]
    private float _currentPowerConsumption;

    public float CurrentPowerConsumption
    {
        get { return _currentPowerConsumption; }
        set
        {
            if (_currentPowerConsumption != value)
            {
                _currentPowerConsumption = value;
                NotifyPropertyChanged();
            }
        }
    }
    public bool IsDeviceOn
    {
        get { return _isDeviceOn; }
        set { 
            if(_isDeviceOn != value)
            {
                _isDeviceOn = value;
                NotifyPropertyChanged();
            }
           
        }
    }
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    private void Awake()
    {
        PowerManager = FindObjectOfType<PowerManagement>();
        if (MaxPowerProduced > 0)
        {
            PowerManager.PowerProduced.Add(this);
        }
        else
        {
            PowerManager.RequiresPower.Add(this);
        }
    }
    private void Start()
    {
        CanBePowered();
    }
    public bool CanBePowered()
    {
        if(IsDeviceOn && MaxRequiredPower > 0)
        {
            if(PowerManager.RequestPower(MaxRequiredPower,CurrentPowerConsumption))
            {
                CurrentPowerConsumption = MaxRequiredPower;
                return true;
            }
            else
            {
                CurrentPowerConsumption = 0;
                return false;
            }
        }
        else
        {
            CurrentPowerConsumption = 0;
            return false;
        }
    }

    private void OnDestroy()
    {
        Destroying = true;
        if (MaxPowerProduced > 0)
        {
            PowerManager.PowerProduced.Remove(this);
        }
        else
        {
            PowerManager.RequiresPower.Remove(this);
        }
    }
}
