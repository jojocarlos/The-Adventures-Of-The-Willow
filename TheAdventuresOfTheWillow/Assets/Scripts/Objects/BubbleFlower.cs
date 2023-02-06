using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleFlower : MonoBehaviour
{
	public AudioClip PowerUpCollect;
	
	private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
			AudioSource.PlayClipAtPoint(PowerUpCollect, transform.position);
			Powers.instance.isBubblePlayerState();
            Destroy(gameObject);
        }
    }
}
