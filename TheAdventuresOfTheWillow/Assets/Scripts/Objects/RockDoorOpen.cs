using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RockDoorOpen : MonoBehaviour
{
    private KeyFollow thePlayer;

    public int doorID;
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
        if (waitingToOpen && !doorOpen)
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


    IEnumerator Opened()
    {
        yield return new WaitForSeconds(seconds);

        theSR.sprite = doorOpenSprite;

        List<Key> keysToRemove = new List<Key>();

        foreach (Key key in thePlayer.followingKeys)
        {
            if (key.keyID == doorID)
            {
                key.gameObject.SetActive(false);
                thePlayer.RemoveKey(key);
                keysToRemove.Add(key);
                key.keyDoorOpened = true;
            }
        }

        foreach (Key key in keysToRemove)
        {
            thePlayer.followingKeys.Remove(key);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (Key key in thePlayer.followingKeys)
            {
                if (key.keyID == doorID)
                {
                    waitingToOpen = true;
                    break;
                }
            }
        }
    }
}
