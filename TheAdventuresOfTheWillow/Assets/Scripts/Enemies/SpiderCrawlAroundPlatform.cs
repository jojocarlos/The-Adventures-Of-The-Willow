using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderCrawlAroundPlatform : MonoBehaviour
{
    private Rigidbody2D rb2d;
    [SerializeField] private bool groundDetect;
    [SerializeField] private bool wallDetect;
    [SerializeField] private Transform groundPositionChecker;
    [SerializeField] private Transform wallPositionChecker;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    private bool hasTurn;
    private float ZaxieAdd;
    private int Direction;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        hasTurn = false;
        Direction = 1;
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        CheckGroundOrWall();
        Movement();
    }

    void CheckGroundOrWall()
    {
        groundDetect = Physics2D.Raycast(groundPositionChecker.position, -transform.up, groundCheckDistance, whatIsGround);
        wallDetect = Physics2D.Raycast(wallPositionChecker.position, transform.right, wallCheckDistance, whatIsGround);

        if (!groundDetect)
        {
            if (hasTurn == false)
            {
                ZaxieAdd -= 90;
                transform.eulerAngles = new Vector3(0, 0, ZaxieAdd);
                if (Direction == 1)
                {
                    transform.position = new Vector2(transform.position.x + 0.3f, transform.position.y - 0.3f);
                    hasTurn = true;
                    Direction = 2;
                }
                else if (Direction == 2)
                {
                    transform.position = new Vector2(transform.position.x - 0.3f, transform.position.y - 0.3f);
                    hasTurn = true;
                    Direction = 3;
                }
                else if (Direction == 3)
                {
                    transform.position = new Vector2(transform.position.x - 0.3f, transform.position.y + 0.3f);
                    hasTurn = true;
                    Direction = 4;
                }
                else if (Direction == 4)
                {
                    transform.position = new Vector2(transform.position.x + 0.3f, transform.position.y + 0.3f);
                    hasTurn = true;
                    Direction = 1;
                }
            }

        }
        if (groundDetect)
        {
            hasTurn = false;
        }
        if (wallDetect)
        {
            if (hasTurn == false)
            {
                ZaxieAdd += 90;
    
            transform.eulerAngles = new Vector3(0, 0, ZaxieAdd);
                if (Direction == 1)
                {
                    transform.position = new Vector2(transform.position.x - 0.2f, transform.position.y + 0.2f);
                    hasTurn = true;
                    Direction = 4;
                }
                else if (Direction == 2)
                {
                    transform.position = new Vector2(transform.position.x + 0.2f, transform.position.y + 0.2f);
                    hasTurn = true;
                    Direction = 1;
                }
                else if (Direction == 3)
                {
                    transform.position = new Vector2(transform.position.x + 0.2f, transform.position.y - 0.2f);
                    hasTurn = true;
                    Direction = 2;
                }
                else if (Direction == 4)
                {
                    transform.position = new Vector2(transform.position.x - 0.2f, transform.position.y - 0.2f);
                    hasTurn = true;
                    Direction = 3;
                }
            }
        }
    }

    void Movement()
    {
        rb2d.velocity = transform.right * 5;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundPositionChecker.position, new Vector2(groundPositionChecker.position.x, groundPositionChecker.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallPositionChecker.position, new Vector2(wallPositionChecker.position.x + wallCheckDistance, wallPositionChecker.position.y));
    }
}
