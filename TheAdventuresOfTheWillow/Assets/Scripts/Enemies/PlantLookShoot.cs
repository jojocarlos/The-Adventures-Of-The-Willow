using System.Collections;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class PlantLookShoot : MonoBehaviour
{
	[SerializeField] private Transform Target;
	
	Vector2 Direction;
	public GameObject Head;
	
	public GameObject Bullet;
	public float FireRate;
	float nextTimeToFire = 0;
	public Transform ShootPoint;
	public float Force;
	private Animator Anim;

    void Start()
    {
        Anim = GetComponentInChildren<Animator>();
        FindTarget();
		Target = PlayerMovement2D.PlayerMovement2Dinstance.transform;
    }

	private void FindTarget()
	{
        Target = PlayerMovement2D.PlayerMovement2Dinstance.transform;
    }

    void FixedUpdate()
    {
		if(Target == null)
		{
			FindTarget();
        }
        Target = PlayerMovement2D.PlayerMovement2Dinstance.transform;

        Vector2 targetPos = Target.transform.position;
		Direction = targetPos - (Vector2)transform.position;
		
		Head.transform.up = Direction;
		if(Time.time > nextTimeToFire)
		{
		    nextTimeToFire = Time.time+1/FireRate;
            shoot();			
		}
		
    }

    void shoot()
	{
        AudioManager.instance.PlayOneShot(FMODEvents.instance.PlantShoot, this.transform.position);
        Anim.SetTrigger("Shoot");
	    GameObject BulletIns = Instantiate(Bullet, ShootPoint.transform.position, Quaternion.identity);
        BulletIns.GetComponent<Rigidbody2D>().AddForce(Direction * Force);		
	}
	
	private void OnCollisionEnter2D(Collision2D col)
	{
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerMovement2D.PlayerMovement2Dinstance.KnockBackCount = PlayerMovement2D.PlayerMovement2Dinstance.KnockBackLength;
            if(col.transform.position.x < transform.position.x)
            {
                PlayerMovement2D.PlayerMovement2Dinstance.KnockFromRight = true;
            }
            else
            {
                PlayerMovement2D.PlayerMovement2Dinstance.KnockFromRight = false;
            }
        }
    }
	
}
