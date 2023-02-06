using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
	public Transform downpos;
	public Transform upperpos;
	
	public float speed;
	public bool iselevatordown;
	
	
    void Update()
    {
        if(iselevatordown)
		{
			transform.position = Vector2.MoveTowards(transform.position,upperpos.position,speed * Time.deltaTime);
		}
		else
		{
			transform.position = Vector2.MoveTowards(transform.position,downpos.position,speed * Time.deltaTime);
		}
    }
	
	private void OnTriggerStay2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
			if(Input.GetKeyDown("e"))
		    {
			    if(transform.position.y <= downpos.position.y)
			    {
				    iselevatordown = true;
			    }
			    else if(transform.position.y >= upperpos.position.y)
			    {
				    iselevatordown = false;
			    }
		    }
        }
    }
	
}
