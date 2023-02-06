using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRemovePowers : MonoBehaviour
{
    public Powers powers;
    
	
	private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
			powers.isNormalState();
        }
    }
}
