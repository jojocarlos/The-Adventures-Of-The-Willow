using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePrefab : MonoBehaviour
{
    private GameObject PlayerPrefab;

    private void Awake()
    {
        PlayerPrefab = Resources.Load<GameObject>("Player");
        Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
    }
}
