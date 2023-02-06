using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpyWalkEnemy : MonoBehaviour
{
     //GroundCheck
    [SerializeField] LayerMask groundLayer;
	
	//move
    [SerializeField] float moveSpeed;
    private float moveDirection = 1;
	
    [Header("Jump System")]
    Vector2 vecGravity;
    public float jumpPower;
    public float jumpTime;
    public float fallMultiplier;
    public float jumpMultiplier;
    public bool isJumping;
    float jumpCounter;
	//checks
    [SerializeField] Transform wallCheckPoint;
    [SerializeField] Transform EnemyCheckPoint;
    [SerializeField] float circleRadius;
    [SerializeField] LayerMask enemyLayer;
	
    private bool checkingWall;
    private bool checkingEnemy;
	
	float currentTimeJump = 0f;
	float startingTimeJump = 0f;
	[SerializeField]private float TimeToJump;
    [SerializeField]private bool facingRight = true;
	
	
	CapsuleCollider2D bc2d;
    Rigidbody2D rb;
	
	void Start()
    {
        vecGravity = new Vector2(0, -Physics.gravity.y);
        rb = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<CapsuleCollider2D>();
		currentTimeJump = startingTimeJump;
    }
	
    void Update()
    {
		currentTimeJump -= 1 * Time.deltaTime;
        
		if (isgrounded() && currentTimeJump <= 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            isJumping = true;
            jumpCounter = 0;
			startingTimeJump = TimeToJump;
		    currentTimeJump = startingTimeJump;
        }
		
        if (rb.velocity.y > 0 && isJumping)
        {
            jumpCounter += Time.deltaTime;
            if (jumpCounter > jumpTime) isJumping = false;
            float t = jumpCounter / jumpTime;
            float currentJumpM = jumpMultiplier;
			
            if (t > 0.5f)
            {
                currentJumpM = jumpMultiplier * (1 - t);
            }

            rb.velocity += vecGravity * jumpMultiplier * Time.deltaTime;
        }
		
        if (rb.velocity.y < 0)
        {
            rb.velocity -= vecGravity * fallMultiplier * Time.deltaTime;
        }
		
		if(currentTimeJump <= 0)
		{
			currentTimeJump = 0;
		}
		
		//Patrol
		if (checkingWall || checkingEnemy)
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
        rb.velocity = new Vector2(moveSpeed * moveDirection, rb.velocity.y);
    }
	
	void FixedUpdate()
    {
        checkingWall = Physics2D.OverlapCircle(wallCheckPoint.position, circleRadius, groundLayer);
		checkingEnemy = Physics2D.OverlapCircle(EnemyCheckPoint.position, circleRadius, enemyLayer);
    }
	
	void Flip()
    {
        moveDirection *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
	
	private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(wallCheckPoint.position, circleRadius);
        Gizmos.DrawWireSphere(EnemyCheckPoint.position, circleRadius);
    }

	private bool isgrounded()
    {
        float extraHeightText = 0.5f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(bc2d.bounds.center, bc2d.bounds.size, 0f, Vector2.down, extraHeightText, groundLayer);
        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(bc2d.bounds.center + new Vector3(bc2d.bounds.extents.x, 0), Vector2.down * (bc2d.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(bc2d.bounds.center - new Vector3(bc2d.bounds.extents.x, 0), Vector2.down * (bc2d.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(bc2d.bounds.center - new Vector3(bc2d.bounds.extents.x, bc2d.bounds.extents.y + extraHeightText), Vector2.right * (bc2d.bounds.extents.x), rayColor);
        //Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
        //return Physics2D.BoxCast(bc2d.bounds.center, bc2d.bounds.size, 0f, Vector2.down, 1f, groundLayer);
    }
}
