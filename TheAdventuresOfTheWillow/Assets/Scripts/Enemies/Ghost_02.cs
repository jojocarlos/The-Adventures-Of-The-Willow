using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_02 : MonoBehaviour
{
    public float speed = 2; //Boo Speed

    [SerializeField] GameObject playerObjects;
	[SerializeField] private Rigidbody2D playerRigidbody;
	private Animator Anim;
    SpriteRenderer spriteRenderer;
	private bool playerIsMoving;
	private bool ghostIsMove;
	private float currentTime = 0f;
	private float startingTime = 0f;
	[SerializeField] private float TimeTo;
	
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
		startingTime = TimeTo;
		Anim = GetComponent<Animator>();
    }

    void Update()
    {
		RotateUpdate();
		
		if(playerRigidbody.velocity.magnitude != 0 && !ghostIsMove)
		{
			playerIsMoving = true;
			startingTime = TimeTo;
		    currentTime = startingTime;
            AudioManager.instance.PlayOneShot(FMODEvents.instance.Ghost01, this.transform.position);
        }
		
		currentTime -= 1 * Time.deltaTime;
		if(currentTime <= 0)
		{
			currentTime = 0;
			playerIsMoving = false;
			ghostIsMove = false;
		}
		
    }

    
    void Move(Vector2 target)
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
    }

    void RotateUpdate()
    {
        FlipRotating();
        if (spriteRenderer == null)
		{
            spriteRenderer = GetComponent<SpriteRenderer>();
		}
		
        if (playerIsMoving && currentTime >= 0)
		{
			Anim.SetBool("isMoving", true);
            spriteRenderer.color = Color.white;
            Move(playerObjects.transform.position);
			ghostIsMove = true;
		}
		else
		{
			Anim.SetBool("isMoving", false);
            Color c = spriteRenderer.color;
            c.a = 0.75f;
            spriteRenderer.color = c;
		}
    }

    void FlipRotating()
    {
        if (this.transform.position.x > playerObjects.transform.position.x)
            Turn(1);
        else
            Turn(-1);
    }

    void Turn(float inputDirection)
    {
        if (inputDirection < 0)
            this.transform.localEulerAngles = new Vector3(0, 180, 0);
        else
            if (inputDirection > 0)
            this.transform.localEulerAngles = new Vector3(0, 0, 0);
        else
            return;
    }
	
	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag ("Player"))
        {
            PlayerMovement2D.PlayerMovement2Dinstance.KnockBackCount = PlayerMovement2D.PlayerMovement2Dinstance.KnockBackLength;
            if (other.transform.position.x < transform.position.x)
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
