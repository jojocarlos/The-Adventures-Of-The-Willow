using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour
{
    [SerializeField] private float frequency;
    [SerializeField] private float amplitude;


    [SerializeField] private float _frequency;
    [SerializeField] private float _amplitude;

    public float followSpeed;
    public Transform followTarget;

    private void FixedUpdate()
    {
        float x = Mathf.Cos(Time.time * _frequency) * _amplitude;
        float y = Mathf.Sin(Time.time * frequency) * amplitude;
        float z = transform.position.z;


    }

}
