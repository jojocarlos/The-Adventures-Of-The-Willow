using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ignorecollider : MonoBehaviour
{
    public string Player = "Ignored";

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == Player)
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}