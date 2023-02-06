using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChameleonAnim : MonoBehaviour
{
	private SpriteRenderer spriteRenderer;
	[SerializeField] [Range(0f, 1f)] float lerpTime;
	[SerializeField] Color myColor, myColor2;
	[SerializeField] private GameObject player;
	public float distanceBetween;
    private float distance;
	private Animator Anim;
	
	private TongueCollision tongueCollision;
	
	void Start()
	{
	    spriteRenderer = GetComponent<SpriteRenderer>();
		Anim = GetComponent<Animator>();
		tongueCollision = GetComponentInChildren<TongueCollision>();
	}
	
	void Update()
	{
		distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		if (distance < distanceBetween)
        {
		    spriteRenderer.material.color = Color.Lerp(spriteRenderer.material.color, myColor, lerpTime);
			Anim.SetBool("Open", true);
		}
		
		if (distance > distanceBetween)
        {
			spriteRenderer.material.color = Color.Lerp(spriteRenderer.material.color, myColor2, lerpTime);	
            Anim.SetBool("Open", false);	
		}
	}
	
	public void AnimTongue()
	{
		tongueCollision.isTongue = true;
	}
	public void AnimFalseTongue()
	{
		tongueCollision.isTongue = false;
	}
	
}
