using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Snail : MonoBehaviour
{
    const string LEFT = "left";
    const string RIGHT = "right";

    [SerializeField] Transform castPos;
    [SerializeField] Transform castWallPos;
    [SerializeField] float baseCastDist;

    string facingDirection;
    Vector3 baseScale;
    Vector2 direction = Vector2.right;

    Rigidbody2D rb2d;

    public float moveSpeed = 0.3f;

    public GameObject collider01;
    public GameObject collider02;
    public GameObject collider03;
    private Animator Anim;

    [SerializeField] private bool isHide;
    [SerializeField] private bool isIdle;

    //Crawn
    [SerializeField] private bool Crawn;
    private bool timeIsCounting;
    [SerializeField] private float toWalkAgain = 1f;
    [SerializeField] private float TimeCrawn = 1f;
	private float currentTime = 0f;
	private float startingTime = 0f;
	
	//playerstate
	RaycastHit2D hit;
	[SerializeField] private float rayplayerDist;
	public Transform playerDetect;

    //kick
    [SerializeField] private float kickForce = 10;
    [SerializeField] private float yForce = 10;

    private bool audioIsPlay;
	
	 [Header("For SeeingPlayer")]
    [SerializeField] Vector2 lineOfSite;
    [SerializeField] Vector2 lineOfSite2;
    [SerializeField] LayerMask playerLayer;
    private bool canSeePlayer;
    private bool canSeePlayerToHide;
    public GameObject Point1;
    public GameObject Point2;
    [SerializeField] private PlayerMovement2D playerMovement2D;

    //Sounds
    private EventInstance SnailSlime;


    void Start()
    {
        baseScale = transform.localScale;
        Anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        startingTime = TimeCrawn;
        facingDirection = RIGHT;
        PlaySound();
        audioIsPlay = true;
        isHide = false;
    }

    private void PlaySound()
    {
        PLAYBACK_STATE playbackState;
        SnailSlime.getPlaybackState(out playbackState);
        if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
        {
            SnailSlime.start();
        }
    }

    private void StopSound()
    {
        SnailSlime.stop(STOP_MODE.ALLOWFADEOUT);
    }

    private void Update()
    {

        if (currentTime >= 0)
        {
            currentTime -= 1 * Time.deltaTime;
        }

        if (currentTime <= 0)
        {
            currentTime = 0;
            Crawn = true;
            StartCoroutine(crawn());
        }
    }
    private void HideState()
	{
		Anim.SetBool("Hide", true);
		isHide = true;
        collider01.SetActive(false);
        collider02.SetActive(false);
        collider03.SetActive(false);
        if (audioIsPlay)
        {
            StopSound();
            audioIsPlay = false;
        }
    }

    private void NoHideState()
    {
        Anim.SetBool("Hide", false);
        isHide = false;
        collider01.SetActive(true);
        collider02.SetActive(true);
        collider03.SetActive(true);
        if (!audioIsPlay)
        {
            PlaySound();
            audioIsPlay = true;
        }
    }

    private void IdleState()
	{
		Anim.SetBool("Idle", true);
        isIdle = true;
        if (audioIsPlay)
        {
            StopSound();
            audioIsPlay = false;
        }
    }

    private void NoIdleState()
    {
        Anim.SetBool("Idle", false);
        isIdle = false;
        if (!audioIsPlay)
        {
            PlaySound();
            audioIsPlay = true;
        }
    }

    IEnumerator crawn()
	{
		yield return new WaitForSeconds(toWalkAgain);
		Crawn = false;
		timeIsCounting = false;
	}

    private void FixedUpdate()
    {
		canSeePlayer = Physics2D.OverlapBox(Point1.transform.position, lineOfSite, 0, playerLayer);
        canSeePlayerToHide = Physics2D.OverlapBox(Point2.transform.position, lineOfSite2, 0, playerLayer);

        if (canSeePlayer)
        {
            IdleState();
        }
        else 
        {
            NoIdleState();
        }
        if(canSeePlayerToHide)
        {
            HideState();
        }
        else
        {
            NoHideState();
        }
		
        float vX = moveSpeed;

        if (facingDirection == LEFT)
        {
            vX = -moveSpeed;
        }

        if (!Crawn && !isHide && !isIdle)
        {
            //move the game object
            rb2d.velocity = new Vector2(vX, rb2d.velocity.y);

            if (!timeIsCounting)
            {
                startingTime = TimeCrawn;
                currentTime = startingTime;
                timeIsCounting = true;
            }
        }


        if (IsHitingWall() || IsNearEdge() || IsHitingEnemy() || IsHitingBox() && !isHide)
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
	
 
    private void OnCollisionEnter2D (Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 direction = (other.transform.position - transform.position).normalized;
            rb2d.velocity = transform.up * yForce;
            rb2d.AddForce(-direction * kickForce, ForceMode2D.Impulse);
        }
        if (other.gameObject.CompareTag("Player"))
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

    
	
	private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Point1.transform.position, lineOfSite);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Point2.transform.position, lineOfSite2);
    }


}
