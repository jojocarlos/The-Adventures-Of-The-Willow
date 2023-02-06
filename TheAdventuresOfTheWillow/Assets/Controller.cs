using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Controller : MonoBehaviour
{
    public float _speed = 1f;
    private Rigidbody2D _rb;
    private float move;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Movement(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>().x;
    }

    void FixedUpdate()
    {
       if(Mathf.Abs(_rb.velocity.x) < _speed)
        {
            if (move <= 0)
            {
                _rb.AddForce(Vector2.right * 50f);
            }
            if (move >= 0)
            {
                _rb.AddForce(- Vector2.right * 50f);
            }
        }
    }
}
