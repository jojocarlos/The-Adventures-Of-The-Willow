using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantShooting : MonoBehaviour
{
    [SerializeField]
    GameObject objectToSpawn;

    public float delayBeforeSpawn = 0;
    public float cycleTime = 3.5f;
    public float forceX = 0;
    public float forcey = 0;
    public float torque = 0;
    public float destroyAfter = 3.5f;
    private GameObject spawnedGameObject;
    private Animator ani;
    private AudioSource audioplay;
    [SerializeField] private PlayerMovement2D playerMovement2D;

   
    
    void Start()
    {
        InvokeRepeating("Spawn", delayBeforeSpawn, cycleTime);
        ani.SetTrigger("plantFire");
		audioplay = GetComponent<AudioSource>();
		ani = GetComponent<Animator>();
    }

    void Spawn()
    {
        audioplay.Play();
        Rigidbody2D rb = null;
        spawnedGameObject = (GameObject)Instantiate(objectToSpawn);
        rb = spawnedGameObject.GetComponent<Rigidbody2D>();
        spawnedGameObject.transform.position = transform.position + Vector3.up * 0.1f;
        rb.AddForce(new Vector2(forceX, forcey));
        rb.AddTorque(torque);
        Destroy(spawnedGameObject, destroyAfter);

        if (forceX < 0)
        {
            spawnedGameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
	private void OnCollisionEnter2D(Collision2D col)
	{
        if (col.gameObject.CompareTag("Player"))
        {
            playerMovement2D.KnockBackCount = playerMovement2D.KnockBackLength;
            if(col.transform.position.x < transform.position.x)
            {
                playerMovement2D.KnockFromRight = true;
            }
            else
            {
                playerMovement2D.KnockFromRight = false;
            }
        }
    }
    
}
   
