using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRotationAnother : MonoBehaviour
{
    private float rotZ;
    public float RotationSpeed;
    public bool ClockWiseRotation;
    // Update is called once per frame
    void Update()
    {
        if(ClockWiseRotation == false)
        {
            rotZ += Time.deltaTime * RotationSpeed;
        }
        else
        {
            rotZ += Time.deltaTime * RotationSpeed;
        }

        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
