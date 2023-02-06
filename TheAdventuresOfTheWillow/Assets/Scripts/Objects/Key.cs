using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private bool isFollowing;

    public float followSpeed;

    public Transform followTarget;
    public Animator Anim;
    

    void Start()
    {
        Anim.SetBool("PlayerFollow", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowing)
        {
            transform.position = Vector3.Lerp(transform.position, followTarget.position, followSpeed * Time.deltaTime); 
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (!isFollowing)
            {
                KeyFollow thePlayer = FindObjectOfType<KeyFollow>();
                Anim.SetTrigger("PlayerFollow");
                followTarget = thePlayer.KeyFollowPoint;
                isFollowing = true;
                thePlayer.followingKey = this;
            }
        }
    }
}
