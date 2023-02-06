using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMovement : MonoBehaviour
{
    public Transform RotationCenter;

    public float AngularSpeed, RotationRadius;

    private float posX, posY, angle = 0;
    private void Update()
    {
        posX = RotationCenter.position.x + Mathf.Cos(angle) * RotationRadius;
        posY = RotationCenter.position.y + Mathf.Sin(angle) * RotationRadius;
        transform.position = new Vector2(posX, posY);
        angle = angle + Time.deltaTime * AngularSpeed;

        if (angle >= 360)
        {
            angle = 0;
        }
    }

}
