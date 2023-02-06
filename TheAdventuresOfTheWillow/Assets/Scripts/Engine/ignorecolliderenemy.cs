using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ignorecolliderenemy : MonoBehaviour
{
    public string FishJump = "Ignored";

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == FishJump)
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}