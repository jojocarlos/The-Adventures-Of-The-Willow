using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesController : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;

    public void ParticlesStart()
    {
        particles.Play();
    }

    public void ParticlesStop()
    {
        particles.Stop();
    }
}
