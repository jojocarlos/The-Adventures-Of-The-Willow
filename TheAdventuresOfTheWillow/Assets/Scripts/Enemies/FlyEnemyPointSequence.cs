using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemyPointSequence : MonoBehaviour
{
    [SerializeField] private float speedMove;
    [SerializeField] private Transform[] movePoints;
    [SerializeField] private float minDistance;

    private int nextStep = 0;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Turn();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, movePoints[nextStep].position, speedMove * Time.deltaTime);

        if (Vector2.Distance(transform.position, movePoints[nextStep].position) < minDistance)
        {
            nextStep += 1;
            if (nextStep >= movePoints.Length)
            {
                nextStep = 0;
            }
            Turn();
        }
    }

    private void Turn()
    {
        if (transform.position.x < movePoints[nextStep].position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
		
    }
}
