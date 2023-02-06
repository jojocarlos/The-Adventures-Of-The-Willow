using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    [SerializeField] private Hive hiveScript;

    //Fly Random 
    [SerializeField] private float speedMove;
    [SerializeField] private Transform[] movePoints;
    [SerializeField] private float minDistance;

    private int randomNumber;
    private SpriteRenderer spriteRenderer;

    //Fly follow to Player
    [SerializeField] private float timeBase;
    private float timeFollow;
    [SerializeField] private Transform player;

    private GameObject thisObject;

    void Start()
    {
        hiveScript = GetComponentInParent<Hive>();
        randomNumber = Random.Range(0, movePoints.Length);
        spriteRenderer = GetComponent<SpriteRenderer>();
        Turn();
        timeFollow = timeBase;
    }

    void Update()
    {
        if (hiveScript.playerIsOnHive)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speedMove * Time.deltaTime);
            timeFollow -= Time.deltaTime;
            if(Vector2.Distance(transform.position, player.position) < minDistance)
            {
                Turn();
            }

            if (timeFollow <= 0)
            {
                hiveScript.playerIsOnHive = false;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, movePoints[randomNumber].position, speedMove * Time.deltaTime);

            if (Vector2.Distance(transform.position, movePoints[randomNumber].position) < minDistance)
            {
                randomNumber = Random.Range(0, movePoints.Length);
                Turn();
            }
        }
    }

    private void Turn()
    {
        if (transform.position.x < movePoints[randomNumber].position.x)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && InvinciblePower.instance.isInvincible)
        {
            speedMove = 0f;
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
        Destroy(thisObject, 5f);
    }
}
