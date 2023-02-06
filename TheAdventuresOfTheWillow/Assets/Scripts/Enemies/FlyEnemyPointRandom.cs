using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemyPointRandom : MonoBehaviour
{
    [SerializeField] private float speedMove;
    [SerializeField] private Transform[] movePoints;
    [SerializeField] private float minDistance;

    private int randomNumber;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        randomNumber = Random.Range(0, movePoints.Length);
        spriteRenderer = GetComponent<SpriteRenderer>();
        Turn();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, movePoints[randomNumber].position, speedMove * Time.deltaTime);
   
        if (Vector2.Distance(transform.position, movePoints[randomNumber].position) < minDistance)
        {
            randomNumber = Random.Range(0, movePoints.Length);
		    Turn();
        }
    }

    private void Turn()
    {
        if (transform.position.x < movePoints[randomNumber].position.x)
	    {
	        spriteRenderer.flipX = true;
	    }
	    else
	    {
	        spriteRenderer.flipX = false;
	    }
    }

}
