using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour
{
    public float TimeToGatherResources = 2.0f;
    public bool ResourcesAvailable;
    public int TotalRock = 0;
    
    private PowerIO powerIO;
    private new Animation animation;
    private new ParticleSystem particleSystem;
    void Start()
    {
        powerIO = GetComponent<PowerIO>();
        animation = GetComponentInChildren<Animation>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        InvokeRepeating(nameof(DrillInterval),TimeToGatherResources, TimeToGatherResources);
    }

    void DrillInterval()
    {
        if(powerIO.CanBePowered())
        {
            TotalRock++;
            if(!animation.IsPlaying(animation.clip.name))
            {
                animation.Play();
            }
            if(!particleSystem.isPlaying)
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
