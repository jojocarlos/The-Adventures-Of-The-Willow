using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompEnemy : MonoBehaviour
{
	public Animator Anim;
	public GameObject enemyObject;
	public GameObject enemyBoxColliderPlayer;
	public GameObject thisObject;
	
	public bool deadnow = false;
	private bool audioPlayed;

    [SerializeField] Vector2 lineOfSite;
    [SerializeField] LayerMask playerLayer;
    private bool stomped;

    private void Start()
	{
		deadnow = false;
		audioPlayed = false;
	}

    private void FixedUpdate()
    {
        stomped = Physics2D.OverlapBox(transform.position, lineOfSite, 0, playerLayer);
        if(stomped)
        {
            PlayerMovement2D.PlayerMovement2Dinstance.enemyStompJump();
            deadnow = true;
            DieStomp();
            if (!audioPlayed)
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.Stomp, this.transform.position);
                audioPlayed = true;
            }
            Destroy(enemyBoxColliderPlayer);
        }
    }
   

    public void DieStomp()
	{
		Destroy (thisObject);
		Destroy (enemyObject, 5f);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, lineOfSite);
    }
}
