using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirFlower : MonoBehaviour
{
	
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.PowerUpCollect, this.transform.position);
            Powers.instance.isAirPlayerState();
			Destroy(gameObject);
        }
    }
}
