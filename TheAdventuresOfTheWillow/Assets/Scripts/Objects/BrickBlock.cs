using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BrickBlock : MonoBehaviour
{
    [SerializeField] private UnityEvent _hit;
	public AudioClip brickSound;
	
    private void OnCollisionEnter2D(Collision2D other)
	{
		if(other.collider.bounds.max.y < transform.position.y
			&& other.collider.bounds.min.x < transform.position.x + 0.5f
			&& other.collider.bounds.max.x > transform.position.x -0.5f
			&& other.collider.tag == "Player")
		{
			AudioSource.PlayClipAtPoint(brickSound, transform.position);
			_hit?.Invoke();
		}
	}
}
