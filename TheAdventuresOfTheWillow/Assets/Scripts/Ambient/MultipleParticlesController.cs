using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleParticlesController : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] particles;

    public void MParticlesStart()
    {
        foreach (ParticleSystem p in particles)
        {
            p.Play();
        }
    }

    public void MParticlesStop()
    {
        foreach (ParticleSystem p in particles)
        {
            p.Stop();
        }
    }
}
    