using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class Grabber : MonoBehaviour
{
    public Transform grabDetect;
    public Transform boxHolder;
    public float rayDist;
	private bool Tograb;

    public void Grabing(InputAction.CallbackContext context)
	{
		if (context.performed)
        {
            Tograb=true;
		}
		if (context.canceled)
		{
            Tograb=false;
		}
	}
	
    private void Update()
    {
        RaycastHit2D grabCheck = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale, rayDist);
       
        if(grabCheck.collider != null && grabCheck.collider.tag == "Box")
        {
            if (Tograb)
            {
                grabCheck.collider.gameObject.transform.parent = boxHolder;
                grabCheck.collider.gameObject.transform.position = boxHolder.position;
                grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true; 
            }
            else
            {
                grabCheck.collider.gameObject.transform.parent = null;
                grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;  
            }
        }
		
    }

}