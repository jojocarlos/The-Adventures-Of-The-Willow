using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalFishSwim : MonoBehaviour
{
    const string LEFT = "left";
    const string RIGHT = "right";
	
    [SerializeField]
    Transform castPos;

    [SerializeField]
    float baseCastDist;

    string facingDirection;
    Vector3 baseScale;

    Rigidbody2D rb2d;

    public float moveSpeed = 5f;


    void Start()
    {
        baseScale = transform.localScale;

        facingDirection = RIGHT;

        rb2d = GetComponent<Rigidbody2D>();

    }


    void Update()
    {

    }

    private void FixedUpdate()
    {
        float vX = moveSpeed;

        if (facingDirection == LEFT)
        {
            vX = -moveSpeed;
        }


        //move the game object
        rb2d.velocity = new Vector2(vX, rb2d.velocity.y);

        if (IsHitingWall())
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

        Vector3 targetPos = castPos.position;
        targetPos.x += castDist;

        Debug.DrawLine(castPos.position, targetPos, Color.blue);

        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
        {
            val = true;
        }
        else
        {
            val = false;
        }

        return val;
    }

    
}
