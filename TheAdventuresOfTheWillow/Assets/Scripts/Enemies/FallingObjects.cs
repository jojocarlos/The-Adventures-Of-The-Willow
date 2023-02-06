using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjects : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D boxCollider2D;
    public float distance;
    bool isFalling = false;
    public Transform point;

    public float tofall;
    private Animator Anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        Anim = GetComponent<Animator>();
    }

    private void Update()
    {
        rayHit();
    }

    void rayHit()
    {
        RaycastHit2D hit = Physics2D.Raycast(point.position, transform.TransformDirection(Vector2.down), distance);
        if (hit.collider.gameObject.CompareTag("Player"))
        {
            Anim.SetTrigger("FallAdvise");
            StartCoroutine(ToFall());
        }
    }

    IEnumerator ToFall()
    { 
        yield return new WaitForSeconds(tofall);
        rb.gravityScale = 5;
        isFalling = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer ("Ground"))
        {
            Destroy(this);
        }
    }

}
