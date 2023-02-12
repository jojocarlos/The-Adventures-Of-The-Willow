using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapshotManager : MonoBehaviour
{
    FMOD.Studio.EventInstance Underwater;
    FMOD.Studio.EventInstance Normal;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag ("Player"))
        {
            Underwater = FMODUnity.RuntimeManager.CreateInstance("snapshot:/Underwater");
            Underwater.start();
            Normal = FMODUnity.RuntimeManager.CreateInstance("snapshot:/Normal");
            Normal.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Underwater = FMODUnity.RuntimeManager.CreateInstance("snapshot:/Underwater");
            Underwater.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            Normal = FMODUnity.RuntimeManager.CreateInstance("snapshot:/Normal");
            Normal.start();
        }
    }
}
