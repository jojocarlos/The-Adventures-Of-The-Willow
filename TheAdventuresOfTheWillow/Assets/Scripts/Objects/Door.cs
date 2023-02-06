using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private KeyFollow thePlayer;

    public SpriteRenderer theSR;
    public Sprite doorOpenSprite;

    public bool doorOpen, waitingToOpen;

    public ParticleSystem collectEffect;
	public Animator AnimKeyOpen;
    public float seconds = 3f;
	
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<KeyFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        if (waitingToOpen)
        {
            if(Vector3.Distance(thePlayer.followingKey.transform.position, transform.position) < 0.1f)
            {
                waitingToOpen = false;

                doorOpen = true;
                AnimKeyOpen.SetTrigger("OpenDoor");
				StartCoroutine(Opened());
                collectEffect.Play(); 
            }
        }
        if (doorOpen && Vector3.Distance(thePlayer.transform.position, transform.position) < 1f && Input.GetAxis("Vertical") > 0.1f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
    }
	
	IEnumerator Opened()
    {
		yield return new WaitForSeconds(seconds);
		
        theSR.sprite = doorOpenSprite;
        thePlayer.followingKey.gameObject.SetActive(false);    
        thePlayer.followingKey = null;
	}
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(thePlayer.followingKey != null)
            {
                thePlayer.followingKey.followTarget = transform;
                waitingToOpen = true;
            }
        }
    }
}
