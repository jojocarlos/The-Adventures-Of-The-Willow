using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegMover : MonoBehaviour
{
    public Transform legTarget;
    public LayerMask groundLayer;
    public float hoverDist;
    public float groundCheckDistance;
    public float legMoveDist;
    public Vector3 halfWayPoint;
    public float liftDistance;

    public float legMovementSpeed;
    public int posIndex;
    public Vector3 targetPoint;
    Vector3 oldPos;
    public bool grounded;
    public LegMover opposingLeg;
    public AudioSource aud;
    // Start is called before the first frame update
    void Start()
    {
       
        aud = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();




        if (Vector2.Distance(transform.position, legTarget.position) > legMoveDist && posIndex == 0 && opposingLeg.grounded == true)
        {
            oldPos = legTarget.position;
            targetPoint = transform.position;
            halfWayPoint = (targetPoint + legTarget.position) / 2;
            halfWayPoint.y += liftDistance;
            posIndex = 1;



        }

        else if (posIndex == 1)
        {

            legTarget.position = Vector3.Lerp(legTarget.position, halfWayPoint, legMovementSpeed * Time.deltaTime);



            if (Vector2.Distance(legTarget.position, halfWayPoint) <= 0.1f)
            {
                posIndex = 2;
            }
        }

        else if (posIndex == 2)
        {

            legTarget.position = Vector3.Lerp(legTarget.position, targetPoint, legMovementSpeed * Time.deltaTime);



            if (Vector2.Distance(legTarget.position, targetPoint) < 0.1f)
            {
                aud.pitch = Random.Range(1.8f, 1.9f);
                aud.Play();
                posIndex = 0;
            }
        }

        if (posIndex == 0)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    public void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector3.down, groundCheckDistance, groundLayer);
        if (hit.collider != null)
        {

            Vector3 point = hit.point; // gets the position where the leg hit something
            point.y += hoverDist;
            transform.position = point;
        }
    }

}