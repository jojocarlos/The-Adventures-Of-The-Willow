using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHealth : MonoBehaviour
{
    public int numberOfHealth;
    
	
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            if (PlayerHealth.PlayerHealthInstance.currentHealth >= 100)
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.HealthCollect, this.transform.position);
                PlayerHealth.PlayerHealthInstance.AddHealth(numberOfHealth);
            }
            Destroy(gameObject);
        }
    }
}
