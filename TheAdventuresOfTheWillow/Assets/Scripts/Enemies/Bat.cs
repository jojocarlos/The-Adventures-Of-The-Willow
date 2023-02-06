using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    [SerializeField] public Transform player;
    [SerializeField] public float distanceplayer;

    public Vector3 initialPoint;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        animator = GetComponent<Animator>();
	    initialPoint = transform.position;
	    spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        distanceplayer = Vector2.Distance(transform.position, player.position);
	    animator.SetFloat("DistancePlayer", distanceplayer);
    }

    public void TurnBat(Vector3 objective)
    {
        if (transform.position.x < objective.x)
	    {
	        spriteRenderer.flipX = false;
	    }
	    else
	    {
	        spriteRenderer.flipX = true;
	    }
    }

}
