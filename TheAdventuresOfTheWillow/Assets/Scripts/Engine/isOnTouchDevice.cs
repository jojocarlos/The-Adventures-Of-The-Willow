using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isOnTouchDevice : MonoBehaviour
{
    void Start()
    {
         gameObject.SetActive(Application.isMobilePlatform);
    }

}
