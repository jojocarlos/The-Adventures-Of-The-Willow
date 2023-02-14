using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PassThroughPlatforms : MonoBehaviour
{
	public float waitTime;
	[SerializeField] private Collider2D col;

	private void Update()
	{
		if (PlayerMovement2D.PlayerMovement2Dinstance.vertical == 0)
		{
			waitTime = 0.5f;
			Physics2D.IgnoreLayerCollision(3, 26, false); 
			col.usedByEffector = true;
		}
		if (PlayerMovement2D.PlayerMovement2Dinstance.vertical < 0)
		{
			if (waitTime <= 0)
			{
				waitTime = 0.5f;

                Physics2D.IgnoreLayerCollision(3, 26, true);
                col.usedByEffector = false;
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
