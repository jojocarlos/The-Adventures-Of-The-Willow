using UnityEngine;
using System.Collections;

public class fireball : MonoBehaviour
{
    [SerializeField] private string Player = "Ignored";
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject exploson;
    [SerializeField] private Vector2 velocity;
    [SerializeField] private float degrees;


    // Use this for initialization
    void Start()
    {
        Destroy(this.gameObject, 10);
        rb = GetComponent<Rigidbody2D>();
        velocity = rb.velocity;

    }

    // Update is called once per frame
    void Update()
    {


        if (rb.velocity.y < velocity.y)
            rb.velocity = velocity;

    }


    void OnCollisionEnter2D(Collision2D col)
    {

        rb.velocity = new Vector2(velocity.x, -velocity.y);


        if (col.collider.tag == "enemy")
        {
            Destroy(col.gameObject);
            Explode();
        }

        
        if (col.contacts[0].normal.x != 0)
        {
            if (Vector2.Angle(col.contacts[0].normal, Vector2.up) > degrees)
            {
                Explode();
            }
        }

        if (col.gameObject.tag == Player)
        {
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

    }

    void Explode()
    {

        Instantiate(exploson, transform.position, Quaternion.identity);

        Destroy(this.gameObject);

    }
}