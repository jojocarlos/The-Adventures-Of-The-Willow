using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPlayerObj : MonoBehaviour
{
    public static FindPlayerObj instance;
    public GameObject PlayerObj;

    private void Awake()
    {
        instance = this;    
    }

    void Start()
    {
        PlayerObj = GameObject.FindGameObjectWithTag("Player");
    }
}
