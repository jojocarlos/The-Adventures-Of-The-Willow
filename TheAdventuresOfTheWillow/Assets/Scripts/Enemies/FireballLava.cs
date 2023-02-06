using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballLava : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float velocity;
    public float time;
    private Animator anim;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (time <= 0)
        {
            FireBall();
            time = 2f;
        }
        else
        {
            time -= Time.deltaTime;
        }

        if (rb2d.velocity.y > 1f)
        {
            anim.SetBool("UP", true);
            anim.SetBool("DOWN", false);
        }
        else if (rb2d.velocity.y < -1)
        {
            anim.SetBool("DOWN", true);
            anim.SetBool("UP", false);
        }
        else
        {
            anim.SetBool("DOWN", false);
            anim.SetBool("UP", false);
        }
    }

    void FireBall()
    {
        rb2d.velocity = transform.up * velocity;
    }
}
