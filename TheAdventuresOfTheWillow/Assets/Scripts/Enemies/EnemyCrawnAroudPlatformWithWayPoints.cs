using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrawnAroudPlatformWithWayPoints : MonoBehaviour
{
    public float moveSpeed;
    public GameObject[] wayPoints;

    int nexWaypoint = 1;
    float distToPoint;

    void Update()
    {
        Move();
    }

    void Move()
    {
        distToPoint = Vector2.Distance(transform.position, wayPoints[nexWaypoint].transform.position);
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[nexWaypoint].transform.position, moveSpeed * Time.deltaTime);
        if (distToPoint < 0.2f)
        {
            TakeTurn();
        }

    }


    void TakeTurn()
    {
        Vector3 currRot = transform.eulerAngles;
        currRot.z += wayPoints[nexWaypoint].transform.eulerAngles.z;
        transform.eulerAngles = currRot;
        ChooseNextWayPoint();
    }

    void ChooseNextWayPoint()
    {
        nexWaypoint++;
        if (nexWaypoint == wayPoints.Length)
        {
            nexWaypoint = 0;
        }
    }
}
