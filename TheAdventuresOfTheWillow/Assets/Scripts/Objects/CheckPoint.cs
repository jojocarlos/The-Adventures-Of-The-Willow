using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private bool AudioPlayed;
	private float timeToPlayAgain = 5f;
	
	private void Start()
	{
		AudioPlayed = false;
	}
	
	private void OnTriggerEnter2D(Collider2D checkpoint)
    {
        if(checkpoint.gameObject.CompareTag("Player"))
        {
			if(!AudioPlayed)
			{
                AudioManager.instance.PlayOneShot(FMODEvents.instance.CheckPoint, this.transform.position);
                AudioPlayed = true;
                StartCoroutine(ToPlayAgain());
            }
	    }
	}
	IEnumerator ToPlayAgain()
	{
        yield return new WaitForSeconds(timeToPlayAgain);
		AudioPlayed = false;
    }
}
