using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovementY : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float RestartTimer;

    private void MoveUp()
    {
        rb.velocity = transform.up * speed;
    }

    private void MoveDown()
    {
        rb.velocity = -transform.up * speed;
    }

    private void Start()
    {
        InvokeRepeating("MoveUp", 0, RestartTimer);
        InvokeRepeating("MoveDown", RestartTimer / 2, RestartTimer);
    }
}
