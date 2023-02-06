using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBubblesPlayer : MonoBehaviour
{
    public ParticleSystem Bubble;
    public CapsuleCollider2D bc2d;

    void Start()
    {
        bc2d.GetComponent<CapsuleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("water") == true)
        {
            Bubble.Play();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("water") == true)
        {
            Bubble.Stop();
        }
    }
}
