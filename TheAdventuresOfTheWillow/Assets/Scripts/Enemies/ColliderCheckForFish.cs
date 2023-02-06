using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCheckForFish : MonoBehaviour
{
    public AIEnemyPlayerFollowWater aIEnemyPlayerFollowWater;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            aIEnemyPlayerFollowWater.leaveWater = false;
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            aIEnemyPlayerFollowWater.leaveWater = true;
        }
    }
}
