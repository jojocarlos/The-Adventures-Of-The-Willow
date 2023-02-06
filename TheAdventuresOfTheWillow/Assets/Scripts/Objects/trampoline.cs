using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trampoline : MonoBehaviour
{
	private Animator Anim;
	
	public PlayerMovement2D playermovement;
	
	void Start()
	{
		Anim = GetComponent<Animator>();
	}
	
	
	void ManagerCollision(GameObject collision)
    {
        if (collision.CompareTag("Player"))
        {
            playermovement.TrampolineGo();
			Anim.SetTrigger("Jumpper");
        }
    }
	
	 private void OnCollisionEnter2D(Collision2D col)
    {
        ManagerCollision(col.gameObject);
	}
	
	 private void OnTriggerEnter2D(Collider2D col)
    {
        ManagerCollision(col.gameObject);
	}
}
