using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Gem : MonoBehaviour
{
    public int GemValue = 1;
    public AudioClip Gemcollect;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
			AudioSource.PlayClipAtPoint(Gemcollect, transform.position);
            GemCollect.instance.ChangeGem(GemValue);
            Destroy(gameObject);
        }
    }
	
	
}
