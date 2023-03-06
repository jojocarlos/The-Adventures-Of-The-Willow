using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : MonoBehaviour
{
    //Normal Music Game starts and area started 1
    private void Start()
    {
        AudioManager.instance.InitializeMusic(FMODEvents.instance.music);
        AudioManager.instance.SetMusicAreaParameter("GameMusic", 1);
    }
}