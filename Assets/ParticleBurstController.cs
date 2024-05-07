using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBurstController : Singleton<ParticleBurstController>
{
    [SerializeField]
    private new ParticleSystem particleSystem;
    
    public void Burst() {
        particleSystem.Play();
    }
}
