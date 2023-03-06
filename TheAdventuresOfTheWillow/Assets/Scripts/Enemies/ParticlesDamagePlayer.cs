using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesDamagePlayer : MonoBehaviour
{
    public int damageAmount = 10;
    public ParticleSystem particles;


    void OnParticleCollision2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth.PlayerHealthInstance.TakeDamage(damageAmount);
        }
    }
}

