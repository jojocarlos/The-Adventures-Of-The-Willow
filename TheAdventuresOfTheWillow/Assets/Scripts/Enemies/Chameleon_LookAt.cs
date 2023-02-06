using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chameleon_LookAt : MonoBehaviour
{
    [SerializeField] private Transform target;
	
    
    void Update()
    {
        Vector3 difference = target.position - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
    }
}
