using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] Rigidbody2D rb;
 

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement(Input.GetAxisRaw("Horizontal"));
    }
    void Movement(float direction)
    {
        rb.velocity = new Vector2(direction * movementSpeed, rb.velocity.y);
    }
}
