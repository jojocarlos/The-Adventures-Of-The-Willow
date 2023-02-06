using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionForce : MonoBehaviour
{
	[SerializeField] private float radio;
	[SerializeField] private float explosionForce;
	
    private void Update()
	{
		Explosion();
	}
    
	private void Explosion()
	{
		Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, radio);
		/*/
    	foreach (Collider2D colisionador in initialObjects)
		{
			BoxExplosion box = colisionador.GetComponent<BoxExplosion>();
			if(box != null)
			{
				box.DestroyBox();
			}
		}*/
     	foreach(Collider2D colisionador in objects)
		{
			Rigidbody2D rb = colisionador.GetComponent<Rigidbody2D>();
			if(rb != null)
			{
			    Vector2 direction = colisionador.transform.position - transform.position;
                float distance = 1 + direction.magnitude;
                float finalForce = 	explosionForce / distance;
                rb.AddForce(direction * finalForce);				
			}
			Destroy(gameObject);
		}
	}
	
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, radio);
	}
}
