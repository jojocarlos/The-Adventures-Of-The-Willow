using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;
using UnityEngine.VFX;

public class Finish : MonoBehaviour
{
    [SerializeField] private VisualEffect visualEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PowerUps.MyInstance.Finish();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            UIPowerUps.MyInstance.HideVictoryCondition();
        }
    }

    public void PlayFireworksEffect()
    {
        visualEffect.Play();
    }

    public void StopFireworksEffect()
    {
        visualEffect.Stop();
    }
}
