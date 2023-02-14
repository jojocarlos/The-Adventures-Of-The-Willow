using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptainMushroomRed : MonoBehaviour
{
    const string LEFT = "left";
    const string RIGHT = "right";

    [SerializeField]
    Transform castPos;
	
	[SerializeField]
    Transform castWallPos;


    [SerializeField]
    float baseCastDist;

    string facingDirection;
    Vector3 baseScale;

    Rigidbody2D rb2d;

    public float moveSpeed = 5f;
	
	private Animator animator;
	
	private GameObject thisObject;
	
	private StompEnemy stompEnemy;
	public float scaleSpeed;
	private bool audioPlayed;


    void Start()
    {
        animator = GetComponent<Animator>();
		thisObject = GetComponent<GameObject>();
		stompEnemy = GetComponentInChildren<StompEnemy>();
		
        baseScale = transform.localScale;

        facingDirection = RIGHT;

        rb2d = GetComponent<Rigidbody2D>();
		audioPlayed = false;
    }


    void Update()
    {
        if (stompEnemy.deadnow)
        {
            thisObject.transform.localScale = new Vector3(0, transform.localScale.y + 0.52f * scaleSpeed,
                                                           transform.localScale.z + 1.909531f * scaleSpeed);
        }
    }

    private void FixedUpdate()
    {
        rb2d.velocity += Physics2D.gravity * Time.fixedDeltaTime; // In PhysX, Acceleration ignores mass
        float rigidbodyDrag = Mathf.Clamp01(1.0f - (rb2d.drag * Time.fixedDeltaTime));
        rb2d.velocity *= rigidbodyDrag;
     
        float vX = moveSpeed;

        if (facingDirection == LEFT)
        {
            vX = -moveSpeed;
        }


        //move the game object
        rb2d.velocity = new Vector2(vX, rb2d.velocity.y);

        if (IsHitingWall() || IsNearEdge() || IsHitingEnemy() || IsHitingBox())
        {
            if (facingDirection == LEFT)
            {
                ChanceFacingDirection(RIGHT);
            }
            else if (facingDirection == RIGHT)
            {
                ChanceFacingDirection(LEFT);
            }
        }
    }


    void ChanceFacingDirection(string newDirection)
    {
        Vector3 newScale = baseScale;

        if (newDirection == LEFT)
        {
            newScale.x = -baseScale.x;
        }
        else if (newDirection == RIGHT)
        {
            newScale.x = baseScale.x;
        }

        transform.localScale = newScale;

        facingDirection = newDirection;
    }

    bool IsHitingWall()
    {
        bool val = false;

        float castDist = baseCastDist;
        //Define the cast distance for left and right
        if (facingDirection == LEFT)
        {
            castDist = -baseCastDist;
        }
        else
        {
            castDist = baseCastDist;
        }
        //determine the target destination based on the cast distance;

        Vector3 targetPos = castWallPos.position;
        targetPos.x += castDist;

        Debug.DrawLine(castWallPos.position, targetPos, Color.blue);

        if (Physics2D.Linecast(castWallPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
        {
            val = true;
        }
        else
        {
            val = false;
        }

        return val;
    }

    bool IsNearEdge()
    {
        bool val = true;

        float castDist = baseCastDist;

        //determine the target destination based on the cast distance;

        Vector3 targetPos = castPos.position;
        targetPos.y -= castDist;

        Debug.DrawLine(castPos.position, targetPos, Color.red);

        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
        {
            val = false;
        }
        else
        {
            val = true;
        }

        return val;
    }

    bool IsHitingEnemy()
    {
        bool val = false;

        float castDist = baseCastDist;
        //Define the cast distance for left and right
        if (facingDirection == LEFT)
        {
            castDist = -baseCastDist;
        }
        else
        {
            castDist = baseCastDist;
        }
        //determine the target destination based on the cast distance;

        Vector3 targetPos = castWallPos.position;
        targetPos.x += castDist;

        Debug.DrawLine(castWallPos.position, targetPos, Color.blue);

        if (Physics2D.Linecast(castWallPos.position, targetPos, 1 << LayerMask.NameToLayer("enemy")))
        {
            val = true;
        }
        else
        {
            val = false;
        }

        return val;
    }
    bool IsHitingBox()
    {
        bool val = false;

        float castDist = baseCastDist;
        //Define the cast distance for left and right
        if (facingDirection == LEFT)
        {
            castDist = -baseCastDist;
        }
        else
        {
            castDist = baseCastDist;
        }
        //determine the target destination based on the cast distance;

        Vector3 targetPos = castPos.position;
        targetPos.x += castDist;

        Debug.DrawLine(castPos.position, targetPos, Color.blue);

        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Grabable")))
        {
            val = true;
        }
        else
        {
            val = false;
        }

        return val;
    }
    private void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.CompareTag("Fireball"))
		{
			moveSpeed = 0f;
			animator.SetTrigger("Killed");
			moveSpeed = 0f;
			if (!audioPlayed)
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.MushroomKilled, this.transform.position);
                audioPlayed = true;
            }
		    Killed();
		}
		if (col.gameObject.CompareTag("BubblePower"))
        {
            moveSpeed = 0f;
			animator.SetTrigger("Killed");
			moveSpeed = 0f;
			if (!audioPlayed)
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.MushroomKilled, this.transform.position);
                audioPlayed = true;
            }
            Killed();
        }
		if (col.gameObject.CompareTag("Player") && InvinciblePower.instance.isInvincible)
        {
			moveSpeed = 0f;
			animator.SetTrigger("Killed");
			moveSpeed = 0f;
			if (!audioPlayed)
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.MushroomKilled, this.transform.position);
                audioPlayed = true;
            }
            Killed();
        }

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


    public void Killed()
	{
		Destroy (thisObject, 5f);
	}
	
	public void boxcollider()
	{
		gameObject.GetComponent<BoxCollider2D>().enabled = false;
	}
	
	public void modeSpeedZero()
	{
		moveSpeed = 0f;
	}
}
