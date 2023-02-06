using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantShoot_01 : MonoBehaviour
{
    private float waitedTime;
	public float waitTimeToAttack = 3f;
	private Animator animator;
	public GameObject bulletPrefab;
	public Transform launchSpawnPoint;
	
    void Start()
    {
        waitedTime = waitTimeToAttack;
		animator = GetComponent<Animator>();
    }


    void Update()
    {
        if(waitedTime<=0)
		{
			waitedTime = waitTimeToAttack;
			animator.SetTrigger("Fire");
			Invoke("LaunchBullet", 0.5f);
		}
		else
		{
			waitedTime -= Time.deltaTime;
		}
    }
	
	public void LaunchBullet()
	{
        AudioManager.instance.PlayOneShot(FMODEvents.instance.PlantShoot, this.transform.position);
        GameObject newBullet;
		newBullet = Instantiate(bulletPrefab, launchSpawnPoint.position, launchSpawnPoint.rotation);
	}
	
	private void OnCollisionEnter2D(Collision2D col)
	{
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerMovement2D.PlayerMovement2Dinstance.KnockBackCount = PlayerMovement2D.PlayerMovement2Dinstance.KnockBackLength;
            if (col.transform.position.x < transform.position.x)
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
