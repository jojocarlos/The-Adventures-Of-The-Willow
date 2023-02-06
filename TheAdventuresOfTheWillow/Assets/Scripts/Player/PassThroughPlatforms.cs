using System.Collections;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.InputSystem;

public class PassThroughPlatforms : MonoBehaviour
{
	public float waitTime;
	private float vertical;
	private Collider2D colObj;
	public void DownPlatform(InputAction.CallbackContext context)
	{
		vertical = context.ReadValue<Vector2>().y;
	}

	private void Start()
	{
		colObj = GetComponent<Collider2D>();
    }

	private void Update()
	{
		if (vertical == 0)
		{
			waitTime = 0.5f;
            Physics2D.IgnoreLayerCollision(3, 26, false);
        }
		if (vertical < 0)
		{
			if (waitTime <= 0)
			{
				waitTime = 0.5f;
				Physics2D.IgnoreLayerCollision(3, 26, true);
				Physics2D.GetIgnoreLayerCollision(3, 26);
			}
			else
			{
				waitTime -= Time.deltaTime;
			}
		}
		if (waitTime == 0)
		{
			waitTime = 0f;
		}
	}
	
}
