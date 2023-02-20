using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, IDataPersistence
{
    //same doorID;
    [SerializeField] private string id;

    private KeyFollow thePlayer;

    public int doorID;

    [SerializeField] private SpriteRenderer theSR;
    [SerializeField] private Sprite doorOpenSprite;

    [SerializeField] private bool doorOpen, waitingToOpen;

    [SerializeField] private ParticleSystem collectEffect;
    [SerializeField] private Animator AnimKeyOpen;
    [SerializeField] private float seconds = 3f;
    [SerializeField] private string nameSceneToLoad;

    void Start()
    {
        thePlayer = FindObjectOfType<KeyFollow>();
    }

    void Update()
    {
        if (waitingToOpen && !doorOpen)
        {
            waitingToOpen = false;
            doorOpen = true;
            AnimKeyOpen.SetTrigger("OpenDoor");
            StartCoroutine(Opened());
            collectEffect.Play();
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

        if (doorOpen && Vector3.Distance(thePlayer.transform.position, transform.position) < 1f && PlayerMovement2D.PlayerMovement2Dinstance.vertical > 0.1f)
        {
            SceneManager.LoadScene(nameSceneToLoad);
        }
    }

    IEnumerator Opened()
    {
        yield return new WaitForSeconds(seconds);
        DataPersistenceManager.instance.SaveGame();
        theSR.sprite = doorOpenSprite;
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

    public void LoadData(GameData data)
    {
        data.doorsOpened.TryGetValue(id, out doorOpen);
        if (doorOpen)
        {
            AnimKeyOpen.SetTrigger("OpenDoor");
            StartCoroutine(Opened());
        }
    }

    public void SaveData(GameData data)
    {
        if (data.doorsOpened.ContainsKey(id))
        {
            data.doorsOpened.Remove(id);
        }
        data.doorsOpened.Add(id, doorOpen);
    }
}
