using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovementX2 : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float RestartTimer;

    private void MoveRight()
    {
        rb.velocity = transform.right * speed;
    }

    private void MoveLeft()
    {
        rb.velocity = -transform.right * speed;
    }

    private void Start()
    {
        InvokeRepeating("MoveRight", 0, RestartTimer);
        InvokeRepeating("MoveLeft", RestartTimer / 2, RestartTimer);
    }
}
