using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindController : MonoBehaviour
{
    public Material[] materials;
    public float windspeed;

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var material in materials)
        {
            material.SetFloat("_WindSpeed", windspeed);
        }
    }
}
