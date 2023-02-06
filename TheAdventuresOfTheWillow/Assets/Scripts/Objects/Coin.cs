using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	public int coinValue = 1;
   

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.coinCollected, this.transform.position);
            CoinCollect.instance.ChangeCoin(coinValue);
            Destroy(gameObject);
        }
    }
	
	
}
