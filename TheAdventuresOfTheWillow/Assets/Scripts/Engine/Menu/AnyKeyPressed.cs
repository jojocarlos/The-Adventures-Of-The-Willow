using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnyKeyPressed : MonoBehaviour
{
    [SerializeField] private GameObject textPressed;
    [SerializeField] private GameObject Buttons;
    
    private void Awake()
    {
        textPressed.SetActive(true);
        Buttons.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current.anyKey.isPressed)
        {
            textPressed.SetActive(false);
            Buttons.SetActive(true);
        }
    }
}
