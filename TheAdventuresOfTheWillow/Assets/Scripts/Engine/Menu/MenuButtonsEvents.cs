using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class MenuButtonsEvents : MonoBehaviour
{
    public void buttonClickAudioPlay()
    {
        AudioManager.instance.PlayOneShot(FMODMenuEvents.instance.buttonClick);
    }

    public void buttonSelectAudioPlay()
    {
        AudioManager.instance.PlayOneShot(FMODMenuEvents.instance.buttonSelect);
    }
}
