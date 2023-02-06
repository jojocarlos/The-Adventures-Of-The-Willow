using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocitySpawnerScript : MonoBehaviour
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
   

    void Start()
    {
        InvokeRepeating("DoSpawn", delayBeforeSpawn, cycleTime);
    }

    void DoSpawn()
    {
        Rigidbody2D rb2d = null;
        spawnedGameObject = (GameObject)Instantiate(objectToSpawn);
        rb2d = spawnedGameObject.GetComponent<Rigidbody2D>();
        spawnedGameObject.transform.position = transform.position + Vector3.up * 0.1f;
        rb2d.AddForce(new Vector2(forceX, forcey));
        rb2d.AddTorque(torque);
        Destroy(spawnedGameObject, destroyAfter);

        if (forceX < 0)
        {
            spawnedGameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

}
