using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BubblePower : MonoBehaviour
{
    bool canShoot;
    public float cooldownpower = 1f;
    public float toImplode = 0.5f;
	public Powers powers;
	public PlayerHealth playerhealth;
	public GameObject BubbleObject;
	public float countingDown;

    public Animator anim;
	
	public void ShootBubble(InputAction.CallbackContext context)
	{
		if (context.performed && canShoot && powers.isBubblePlayer == true)
		{
			if(countingDown <= 0)
			{
			    anim.SetBool("Idle", false);
			    explode();
				countingDown = 5f;
			}
		}
	}
	
	void Start()
	{
		canShoot = true;
        BubbleObject.SetActive(false);
		anim.SetBool("Idle", false);
        anim.SetBool("Explode", false);
    }
	void Update()
	{
		countingDown -= 1 * Time.deltaTime;
		if(countingDown <= 0)
		{
			countingDown = 0;
		}
	}
	/*/old
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && canShoot && powers.isBubblePlayer == true)
        {
		    anim.SetBool("Idle", false);
			explode();
        }
    }
	*/
	
	public void explode()
	{
		BubbleObject.SetActive(true);
		canShoot = false;
        anim.SetBool("Explode", true);
		playerhealth.isExplode = true;
        StartCoroutine(Implode());
    }
	
	IEnumerator Implode()
    {
		yield return new WaitForSeconds(toImplode);
        anim.SetBool("Explode", false);
		anim.SetBool("Idle", true);
        canShoot = true;
    }
	
	public void implode()
	{
		playerhealth.isExplode = false;
	    BubbleObject.SetActive(false);
	}
	
	
	
}
