using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTeleporter : MonoBehaviour
{
    private GameObject currentTeleporter;
	public Animator Anim;
	public float waitTime = 1f;

    public void EnterTeleporter(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			if (currentTeleporter != null)
            {
				Anim.SetBool("EnterDoor", true);
                AudioManager.instance.PlayOneShot(FMODEvents.instance.Teleport01, this.transform.position);
                StartCoroutine(Enter());
            }
		}
	} 
	/*/old
	
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentTeleporter != null)
            {
				Anim.SetBool("EnterDoor", true);
                StartCoroutine(Enter());
            }
        }
    }
	*/
	IEnumerator Enter()
    {
		yield return new WaitForSeconds(waitTime);
        transform.position = currentTeleporter.GetComponent<Teleporter>().GetDestination().position;
		exitportal();
    }
	
	private void exitportal()
	{
		Anim.SetBool("EnterDoor", false);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            currentTeleporter = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            if (collision.gameObject == currentTeleporter)
            {
                currentTeleporter = null;
            }
        }
    }
}
