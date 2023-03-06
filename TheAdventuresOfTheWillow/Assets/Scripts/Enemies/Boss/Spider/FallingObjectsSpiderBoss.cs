using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectsSpiderBoss : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsPrefabs;
    [SerializeField] private int numberOfRocks = 10;
    [SerializeField] private float spawnWidth = 10f;
    [SerializeField] private float spawnHeight = 10f; 


    public void SpawnFallNow()
    {
        for (int i = 0; i < numberOfRocks; i++)
        {
            GameObject rockPrefab = objectsPrefabs[Random.Range(0, objectsPrefabs.Length)];
            Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-spawnWidth / 2f, spawnWidth / 2f), Random.Range(-spawnHeight / 2f, spawnHeight / 2f), 0f);

            GameObject newRock = Instantiate(rockPrefab, spawnPosition, Quaternion.identity);

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawLine(transform.position + new Vector3(-spawnWidth / 2f, -spawnHeight / 2f), transform.position + new Vector3(spawnWidth / 2f, -spawnHeight / 2f));
        Gizmos.DrawLine(transform.position + new Vector3(spawnWidth / 2f, -spawnHeight / 2f), transform.position + new Vector3(spawnWidth / 2f, spawnHeight / 2f));
        Gizmos.DrawLine(transform.position + new Vector3(spawnWidth / 2f, spawnHeight / 2f), transform.position + new Vector3(-spawnWidth / 2f, spawnHeight / 2f));
        Gizmos.DrawLine(transform.position + new Vector3(-spawnWidth / 2f, spawnHeight / 2f), transform.position + new Vector3(-spawnWidth / 2f, -spawnHeight / 2f));
    }
}
