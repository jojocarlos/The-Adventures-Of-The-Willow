using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRotationAnotherToActivate : MonoBehaviour
{
    private float rotZ;
    public float RotationSpeed;
    public bool ClockWiseRotation;
	
	public RockDoorOpen rockDoorOpen;
	
    void Update()
    {
		if(rockDoorOpen.startRotation==true)
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
	
}
