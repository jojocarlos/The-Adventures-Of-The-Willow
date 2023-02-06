using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemyPlayerFollowWater : MonoBehaviour
{
    private GameObject player;
    public GameObject Point;
    public float speed;
    public float distanceBetween;

    private float distance;
    public bool leaveWater;

    private void Start()
    {
        leaveWater = true;
		player = GameObject.FindGameObjectWithTag("Player");
    }
    void FixedUpdate()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (!leaveWater)
        {
            if (distance < distanceBetween)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            }

            if (distance > distanceBetween)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, Point.transform.position, speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(Vector3.forward * angle);

            }
        }

        if (leaveWater)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, Point.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    
    }
}
