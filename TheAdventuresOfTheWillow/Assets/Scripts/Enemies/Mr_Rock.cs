using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mr_Rock : MonoBehaviour
{
    public Transform[] patrolPoints;
	public float moveSpeed;
	public int patrolDestination;
	private SpriteRenderer enemySprite;
	
	void Start()
	{
		enemySprite = GetComponentInChildren<SpriteRenderer>();
	}
	
	void Update()
	{
		if(patrolDestination == 0)
		{
			transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);
			if(Vector2.Distance(transform.position, patrolPoints[0].position) < .2f)
			{
				enemySprite.flipX = false;
				patrolDestination = 1;
			}
		}
		
		if(patrolDestination == 1)
		{
			transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);
			if(Vector2.Distance(transform.position, patrolPoints[1].position) < .2f)
			{
				enemySprite.flipX =true;
				patrolDestination = 0;
			}
		}
		
	}
	
}
