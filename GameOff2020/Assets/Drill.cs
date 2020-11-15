using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour
{
    public float TimeToGatherResources = 2.0f;
    public bool ResourcesAvailable;
    
    private PowerIO powerIO;
    private new Animation animation;
    private new ParticleSystem particleSystem;
    private InventoryManager inventoryManager;
    void Start()
    {
        powerIO = GetComponent<PowerIO>();
        inventoryManager = GetComponent<InventoryManager>();
        animation = GetComponentInChildren<Animation>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        InvokeRepeating(nameof(DrillInterval),TimeToGatherResources, TimeToGatherResources);
    }

    void DrillInterval()
    {
        if(powerIO.CanBePowered())
        {
            try
            {
                //Just adding iron for now, will make this choose a random element
                inventoryManager.AddItem(new Iron());

                if (!animation.IsPlaying(animation.clip.name))
                {
                    animation.Play();
                }
                if (!particleSystem.isPlaying)
                {
                    particleSystem.Play();
                }
            }
            catch(InventoryFullException)
            {
                //Inventory is full 
            }
        }
        else
        {
            particleSystem.Stop();
            animation.Stop();
        }
    }
}
