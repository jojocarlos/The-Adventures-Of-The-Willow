using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptainMushroomBlueAlternative : MonoBehaviour
{
    [Header("For Petrolling")]
    [SerializeField] float moveSpeed;
    private float moveDirection = 1;
    private bool facingRight = true;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] Transform wallCheckPoint;
    [SerializeField] Transform EnemyCheckPoint;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] float circleRadius;
    private bool checkingGround;
    private bool checkingWall;
    private bool checkingEnemy;

    [Header("For JumpAttacking")]
    [SerializeField] float jumpHeight;
    [SerializeField] Transform player;
    [SerializeField] Transform groundCheck;
    [SerializeField] Vector2 boxSize;
    private bool isGrounded;

    [Header("For SeeingPlayer")]
    [SerializeField] Vector2 lineOfSite;
    [SerializeField] LayerMask playerLayer;
    private bool canSeePlayer;
    [Header("Other")]
    private Animator enemyAnim;
    public Rigidbody2D enemyRB;
	
	public GameObject thisObject;
	public StompEnemy stompEnemy;
	public Animator animator;
	public float scaleSpeed;
	
    void Start()
    {
        enemyAnim = GetComponent<Animator>();        
    }
	
    void Update()
    {
        if(stompEnemy.deadnow)
		{
			thisObject.transform.localScale = new Vector3(0, transform.localScale.y + 0.52f * scaleSpeed,
                                                           transform.localScale.z + 1.909531f * scaleSpeed);
		}
    }
   
    void FixedUpdate()
    {
        checkingGround = Physics2D.OverlapCircle(groundCheckPoint.position, circleRadius, groundLayer);
        checkingWall = Physics2D.OverlapCircle(wallCheckPoint.position, circleRadius, enemyLayer);
		checkingEnemy = Physics2D.OverlapCircle(EnemyCheckPoint.position, circleRadius, groundLayer);
        isGrounded = Physics2D.OverlapBox(groundCheck.position, boxSize, 0, groundLayer);
        canSeePlayer = Physics2D.OverlapBox(transform.position, lineOfSite, 0, playerLayer);
        AnimationController();
        if (!canSeePlayer && isGrounded)
        {
            Petrolling();
        }
       
       
    }

    void Petrolling()
    {
        if (!checkingGround || checkingWall || checkingEnemy)
        {
            if (facingRight)
            {
                Flip();
            }
            else if (!facingRight)
            {
                Flip();
            }
        }
        enemyRB.velocity = new Vector2(moveSpeed * moveDirection, enemyRB.velocity.y);
    }


    void JumpAttack()
    {
        float distanceFromPlayer = player.position.x - transform.position.x;

        if (isGrounded)
        {
            enemyRB.AddForce(new Vector2(distanceFromPlayer, jumpHeight), ForceMode2D.Impulse);
        }
    }

    void FlipTowardsPlayer()
    {
        float playerPosition = player.position.x - transform.position.x;
        if (playerPosition<0 && facingRight)
        {
            Flip();
        }
        else if (playerPosition>0 && !facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        moveDirection *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    void AnimationController()
    {
        enemyAnim.SetBool("canSeePlayer", canSeePlayer);
        enemyAnim.SetBool("isGrounded", isGrounded);
    }
    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheckPoint.position, circleRadius);
        Gizmos.DrawWireSphere(wallCheckPoint.position, circleRadius);
        Gizmos.DrawWireSphere(EnemyCheckPoint.position, circleRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawCube(groundCheck.position, boxSize);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, lineOfSite);

    }
	
	private void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag.Equals("Fireball"))
		{
			animator.SetTrigger("Killed");
			moveSpeed = 0f;
		    Killed();
		}
		if (col.gameObject.CompareTag("BubblePower"))
        {
			animator.SetTrigger("Killed");
			moveSpeed = 0f;
            Killed();
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
