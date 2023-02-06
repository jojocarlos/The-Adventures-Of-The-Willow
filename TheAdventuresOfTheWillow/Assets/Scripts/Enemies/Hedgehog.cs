using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hedgehog : MonoBehaviour
{
     const string LEFT = "left";
    const string RIGHT = "right";

    [SerializeField] Transform castPos;
    [SerializeField] Transform castWallPos;
    [SerializeField] Transform castWallAttackPos;
    [SerializeField] float baseCastDist;

    string facingDirection;
    Vector3 baseScale;
    Vector2 direction = Vector2.right;

    Rigidbody2D rb2d;

    public float moveSpeed = 1f;
    public float moveAttackSpeed = 10f;
    public float moveAttackUpSpeed = 5f;

    private Animator Anim;

    [SerializeField] private bool OnAttack;
    [SerializeField] private bool isAttacking;
    public float toWalkState = 3f;
	
    private AudioSource audioSource;
    private bool audioIsPlay;
	
	[Header("For SeeingPlayer")]
    [SerializeField] Vector2 lineOfSite;
    [SerializeField] LayerMask playerLayer;
    private bool canSeePlayer;
    public GameObject Point1;


    void Start()
    {
        baseScale = transform.localScale;
        Anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        facingDirection = RIGHT;
		OnAttack = false;
        isAttacking = false;
    }
	
    void Update()
    {
        if(OnAttack && !isAttacking)
        {
            isAttacking = true;
            Anim.SetBool("isAttack", true);
            StartCoroutine (AttackNow());
        }
    }
   
    IEnumerator AttackNow()
    {
        yield return new WaitForSeconds (toWalkState);
        isAttacking = false;
        OnAttack = false;
        Anim.SetBool("isAttack", false);
    }


    private void AttackState()
    {
        OnAttack = true;
    }

    private void FixedUpdate()
    {
		canSeePlayer = Physics2D.OverlapBox(Point1.transform.position, lineOfSite, 0, playerLayer);

        if (canSeePlayer)
        {
            AttackState();
        }
       
        float vX = moveSpeed;
        float vaX = moveAttackSpeed;
        float vay = moveAttackUpSpeed;

        if (facingDirection == LEFT)
        {
            vX = -moveSpeed;
            vaX = -moveAttackSpeed;
            vay = -moveAttackUpSpeed;
        }

        if (OnAttack && isAttacking)
        {
            Anim.SetBool("isAttack", true);
            rb2d.velocity = new Vector2(vaX, rb2d.velocity.y);
            rb2d.gravityScale = 0.3f;
           if(IsHitingWallAttack())
            {
                rb2d.AddForce(transform.up * vay, ForceMode2D.Force);
            }
        }

        if (!OnAttack && !isAttacking)
        {
            rb2d.gravityScale = 1f;
            //move the game object
            Anim.SetBool("isAttack", false);
            rb2d.velocity = new Vector2(vX, rb2d.velocity.y);
        }


        if (IsHitingWall() || IsNearEdge() || IsHitingEnemy() || IsHitingBox())
        {
            if (rb2d.velocity.y <= 0)
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
    }


    void ChanceFacingDirection(string newDirection)
    {
        Vector3 newScale = baseScale;

        if (newDirection == LEFT)
        {
            newScale.x = -baseScale.x; 
            direction = -direction;
        }
        else if (newDirection == RIGHT)
        {
            newScale.x = baseScale.x;
            direction = Vector2.right;
        }

        transform.localScale = newScale;

        facingDirection = newDirection;
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

    bool IsHitingWallAttack()
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

        Vector3 targetPos = castWallAttackPos.position;
        targetPos.x += castDist;

        Debug.DrawLine(castWallAttackPos.position, targetPos, Color.blue);

        if (Physics2D.Linecast(castWallAttackPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
        {
            val = true;
        }
        else
        {
            val = false;
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
	private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Point1.transform.position, lineOfSite);
    }
}
