using System;
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
    private bool onMineralPatch;
    private System.Random rnd;
    void Start()
    {
        rnd = new System.Random();
        powerIO = GetComponent<PowerIO>();
        inventoryManager = GetComponent<InventoryManager>();
        animation = GetComponentInChildren<Animation>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        InvokeRepeating(nameof(DrillInterval), TimeToGatherResources, TimeToGatherResources);

        onMineralPatch = true; //do raycast down and if on patch set this to true

    }


    IElement SelectItem(List<IElement> items)
    {
        int poolSize = 0;
        for (int i = 0; i < items.Count; i++)
        {
            poolSize += items[i].chance;
        }

        int randomNumber = rnd.Next(0, poolSize) + 1;

        int accumulatedProbability = 0;
        for (int i = 0; i < items.Count; i++)
        {
            accumulatedProbability += items[i].chance;
            if (randomNumber <= accumulatedProbability)
                return items[i];
        }
        return null;    
    }



    void DrillInterval()
    {
        if (powerIO.CanBePowered())
        {
            try
            {
                var potentialElements = new List<IElement>();
                potentialElements.Add(new Silicon());
                potentialElements.Add(new Aluminium());
                if (onMineralPatch)
                {
                    potentialElements.Add(new Magnesium());
                    potentialElements.Add(new Titanium());
                    potentialElements.Add(new Iron());
                }

                inventoryManager.AddItem(SelectItem(potentialElements));

                if (!animation.IsPlaying(animation.clip.name))
                {
                    animation.Play();
                }
                if (!particleSystem.isPlaying)
                {
                    particleSystem.Play();
                }
            }
            catch (InventoryFullException)
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
