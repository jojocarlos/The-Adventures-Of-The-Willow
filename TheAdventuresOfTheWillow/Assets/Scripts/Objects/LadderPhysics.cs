using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderPhysics : MonoBehaviour
{


    [Header("ladder")]
    private float vertical;
    public float speedladder = 8f;
    private bool isLadder;
    private bool isClibimg;

    public Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        vertical = Input.GetAxis("Vertical");

        if (isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClibimg = true;
        }
    }
    private void FixedUpdate()
    {
        if (isClibimg)
        {
            rb.velocity = new Vector2(rb.velocity.x, vertical * speedladder);
        }
        else
        {
           
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isLadder = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isLadder = false;
            isClibimg = false;
        }
    }
}
