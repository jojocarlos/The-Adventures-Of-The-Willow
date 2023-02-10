using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerParticlesPlay : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleToPlay;
    [SerializeField] private GameObject objectPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            particleToPlay.Play();
            AudioManager.instance.PlayOneShot(FMODEvents.instance.BirdsFlutter, objectPosition.transform.position);
        }
    }

}
