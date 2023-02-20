using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IDataPersistence
{
    //same KeyID;
    [SerializeField] private string id;
    public int keyID;

    private bool isFollowing;

    public float followSpeed;

    public Transform followTarget;
    public Animator Anim;
    public bool keyDoorOpened;

    void Start()
    {
        Anim.SetBool("PlayerFollow", false);
    }

    void Update()
    {
        if (isFollowing)
        {
            transform.position = Vector3.Lerp(transform.position, followTarget.position, followSpeed * Time.deltaTime);
        }
        if (keyDoorOpened)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isFollowing)
            {
                KeyFollow thePlayer = FindObjectOfType<KeyFollow>();
                Anim.SetTrigger("PlayerFollow");
                followTarget = thePlayer.KeyFollowPoint;
                isFollowing = true;
                thePlayer.AddKey(this);
            }
        }
    }
    public void LoadData(GameData data)
    {
        data.keyDoorOpened.TryGetValue(id, out keyDoorOpened);
        if (keyDoorOpened)
        {
            Destroy(gameObject);
        }
    }

    public void SaveData(GameData data)
    {
        if (data.keyDoorOpened.ContainsKey(id))
        {
            data.keyDoorOpened.Remove(id);
        }
        data.keyDoorOpened.Add(id, keyDoorOpened);
    }
}

