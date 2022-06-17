using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;
    [SerializeField] ParticleSystem particle1;
    [SerializeField] ParticleSystem particle2;
    void Start()
    {
        particle.Play();
        particle1.Play();
        particle2.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
