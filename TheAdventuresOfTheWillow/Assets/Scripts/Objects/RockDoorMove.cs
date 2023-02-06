using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDoorMove : MonoBehaviour
{
	public Transform target;
    public float speed;
	public bool ismoved;
	
	private void Start()
	{
		ismoved = false;
	}
	private void Update()
	{
		if(ismoved)
		{
		    transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
		}
	}
}
