using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODMenuEvents : MonoBehaviour
{
    [field: Header("ButtonSFX")]
    [field: SerializeField] public EventReference buttonClick { get; private set; }
    [field: SerializeField] public EventReference buttonSelect { get; private set; }

    public static FMODMenuEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMOD Events instance in the scene.");
        }
        instance = this;
    }
}
