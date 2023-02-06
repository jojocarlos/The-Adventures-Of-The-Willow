using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCheckForShake : MonoBehaviour
{
    public bool stay;

    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && stay == true)
        {
            CinemachineShake.Instance.ShakeCamera(5f, .1f);
            Debug.Log("Is enter");
        }
    }
}
