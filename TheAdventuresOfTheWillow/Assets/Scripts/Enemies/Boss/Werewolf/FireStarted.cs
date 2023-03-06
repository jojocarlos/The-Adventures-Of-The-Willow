using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStarted : MonoBehaviour
{
    [SerializeField] private Animator fireAnim;
    public void FireStart()
    {
        fireAnim.SetBool("StartFire", true);
    }
}
