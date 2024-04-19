using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: MOVE THIS TO A DELEGATE
public class ParticleBurstController : Singleton<ParticleBurstController>
{
    [SerializeField]
    private new ParticleSystem particleSystem;
    
    public void Burst() {
        particleSystem.Play();
    }
}
