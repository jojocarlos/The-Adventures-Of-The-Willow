using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDoorOpen : MonoBehaviour
{
    private KeyFollow thePlayer;

    public SpriteRenderer theSR;
    public Sprite doorOpenSprite;

    public bool doorOpen, waitingToOpen;

    public ParticleSystem collectEffect;
	public Animator AnimKeyOpen;
    public float seconds = 3f;
	public RockDoorMove movedbool;
	
	public SFX_Ambience sfxToPlay;
	public bool startRotation;
	
    void Start()
    {
        thePlayer = FindObjectOfType<KeyFollow>();
		startRotation = false;
    }

   
    void Update()
    {
        if (waitingToOpen)
        {
            if(Vector3.Distance(thePlayer.followingKey.transform.position, transform.position) < 0.1f)
            {
                waitingToOpen = false;

		        sfxToPlay.startAudio();
                doorOpen = true;
                AnimKeyOpen.SetTrigger("OpenDoor");
				startRotation = true;
				StartCoroutine(Opened());
                collectEffect.Play(); 
            }
        }
    }
	
	IEnumerator Opened()
    {
		yield return new WaitForSeconds(seconds);
		
		movedbool.ismoved = true;
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
