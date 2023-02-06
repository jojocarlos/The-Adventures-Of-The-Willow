using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogJump : MonoBehaviour
{
     //GroundCheck
    [SerializeField] LayerMask groundLayer;
	
    [Header("Jump System")]
    Vector2 vecGravity;
    public float jumpPower;
    public float jumpTime;
    public float fallMultiplier;
    public float jumpMultiplier;
    public bool isJumping;
    float jumpCounter;
	
	float currentTimeJump = 0f;
	float startingTimeJump = 0f;
	[SerializeField]private float TimeToJump;
	
	private Animator Anim;
	
	CapsuleCollider2D bc2d;
    Rigidbody2D rb;

    private PlayerMovement2D playerMovement2D;
    private GameObject PlayerGameObject;

    void Start()
    {
        PlayerGameObject = GameObject.FindGameObjectWithTag("Player");
        playerMovement2D = PlayerGameObject.GetComponent<PlayerMovement2D>();
        vecGravity = new Vector2(0, -Physics.gravity.y);
        rb = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<CapsuleCollider2D>();
		currentTimeJump = startingTimeJump;
		Anim = GetComponent<Animator>();
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
			Anim.SetTrigger("Jump");
            AudioManager.instance.PlayOneShot(FMODEvents.instance.FrogJump, this.transform.position);
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
		
    }
	
	private void OnCollisionEnter2D(Collision2D col)
	{
        if (col.gameObject.CompareTag("Player"))
        {
            playerMovement2D.KnockBackCount = playerMovement2D.KnockBackLength;
            if(col.transform.position.x < transform.position.x)
            {
                playerMovement2D.KnockFromRight = true;
            }
            else
            {
                playerMovement2D.KnockFromRight = false;
            }
        }
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
