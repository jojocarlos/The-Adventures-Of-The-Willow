using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotationPendulum2 : MonoBehaviour
{
    Rigidbody2D rb2d;
	
	public float moveSpeed;
	
	bool movingClockwise;
	
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
		rb2d.angularVelocity = moveSpeed;
    }
}
