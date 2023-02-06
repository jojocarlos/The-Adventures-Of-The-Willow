using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBullet : MonoBehaviour
{
    public float speed = 2f;
	public float lifeTime = 2f;
	public bool left;
	
	private float rotZ;
    public float RotationSpeed;
    public bool ClockWiseRotation;
	
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        if(left)
		{
			transform.Translate(Vector2.left*speed*Time.deltaTime);
		}
		else
		{
			transform.Translate(Vector2.right*speed*Time.deltaTime);
		}
		
		if(ClockWiseRotation == false)
        {
            rotZ += Time.deltaTime * RotationSpeed;
        }
		else
        {
            rotZ += Time.deltaTime * RotationSpeed;
        }
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
