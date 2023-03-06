using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBossArea : MonoBehaviour
{
    [HideInInspector] public bool playerInArea = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInArea = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInArea = false;
        }
    }

    public bool IsPlayerInArea()
    {
        return playerInArea;
    }
}
