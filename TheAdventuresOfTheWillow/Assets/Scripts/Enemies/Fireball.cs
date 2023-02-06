using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public GameObject thisObject;

    void OnCollisionEnter2D(Collision2D col)
	{
		Destroy(thisObject.gameObject);
    }
    
}
