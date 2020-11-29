using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Drill : MonoBehaviour
{
    public float TimeToGatherResources = 2.0f;
    public bool ResourcesAvailable;

    private PowerIO powerIO;
    private new Animation animation;
    private new ParticleSystem particleSystem;
    private Storage storage;
    public bool onMineralPatch;
    public bool onIce;
    private System.Random rnd;
    private List<(InventoryItem, int chance)> elements;
    void Start()
    {
        rnd = new System.Random();
        elements = new List<(InventoryItem, int chance)>();

        powerIO = GetComponent<PowerIO>();
        storage = GetComponentInChildren<Storage>();
        animation = GetComponentInChildren<Animation>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        InvokeRepeating(nameof(DrillInterval), TimeToGatherResources, TimeToGatherResources);

        //onMineralPatch = true; //do raycast down and if on patch set this to true
        //onIce = false; //do raycast down and if on patch set this to true

    }


    InventoryItem SelectItem(List<(InventoryItem, int chance)> items)
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
                return items[i].Item1;
        }
        return null;
    }



    void DrillInterval()
    {
        var hitColliders = Physics.OverlapBox(transform.position, new Vector3(1f, 1f, 1f), transform.rotation);
        bool onMineralPatch = hitColliders.Any(x => x.gameObject.CompareTag("IronPatch"));
        bool onIce = hitColliders.Any(x => x.gameObject.CompareTag("IcePatch"));
        if (powerIO.CanBePowered() && storage.Inventory.CanAddItem())
        {
            if (onIce)
            {
                elements.Add((new InventoryItem(Item.Oxygen), 50));
                elements.Add((new InventoryItem(Item.Hydrogen), 40));
            }
            else
            {
                elements.Add((new InventoryItem(Item.Silicon), 45));
                elements.Add((new InventoryItem(Item.Aluminium), 15));

                if (onMineralPatch)
                {
                    elements.Add((new InventoryItem(Item.Magnesium), 9));
                    elements.Add((new InventoryItem(Item.Titanium), 4));
                    elements.Add((new InventoryItem(Item.Iron), 14));
                }
            }

            storage.Inventory.AddItem(SelectItem(elements));
            storage.ReDraw();
            if (!animation.IsPlaying(animation.clip.name))
            {
                animation.Play();
            }
            if (!particleSystem.isPlaying)
            {
                particleSystem.Play();
            }


        }
        else
        {
            particleSystem.Stop();
            animation.Stop();
        }
    }
}
