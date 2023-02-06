using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovementX : MonoBehaviour
{
    [SerializeField]
    float MovementRadius = 100f, MovementSpeed = 0.07f;

    float posX, number1 = 0f;

    private bool movingRight = true;


    void Update()
    {
        number1 = number1 + 1f;
        if (movingRight == true) posX = transform.position.x + MovementSpeed;
        if (movingRight == false) posX = transform.position.x - MovementSpeed;

        transform.position = new Vector2(posX, transform.position.y);


        if (number1 >= MovementRadius / 2)
            movingRight = false;
        if (number1 >= MovementRadius)
            movingRight = true;
        if (number1 >= MovementRadius)
            number1 = 0f;


    }
}
