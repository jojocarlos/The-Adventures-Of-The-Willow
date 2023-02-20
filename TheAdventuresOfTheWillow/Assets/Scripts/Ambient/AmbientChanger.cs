using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientChanger : MonoBehaviour
{
    public enum TriggerType
    {
        Day,
        Night
    }

    [SerializeField] private Animator Anim;
    [SerializeField] private Animator AnimBack;
    [SerializeField] private TriggerType triggerType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (triggerType == TriggerType.Day)
            {
                Anim.SetBool("IsDay", true);
                AnimBack.SetBool("BackIsDay", true);
            }
            if (triggerType == TriggerType.Night)
            {
                Anim.SetBool("IsDay", false);
                AnimBack.SetBool("BackIsDay", false);
            }
        }
    }

}


