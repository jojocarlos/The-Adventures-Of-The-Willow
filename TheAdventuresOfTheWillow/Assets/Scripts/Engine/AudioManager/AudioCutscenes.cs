using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCutscenes : MonoBehaviour
{
    public enum AreaType
    {
        Spider,
        Werewolf,
        Troll
    }

    private bool initialized;
    [SerializeField] private AreaType areaType;

    public void cutSceneStart()
    {
        if (!initialized)
        {
            AudioManager.instance.SetMusicAreaParameter("GameMusic", 0);
            AudioManager.instance.InitializeFightMusic(FMODEvents.instance.FightMusic);
            if(areaType == AreaType.Spider)
            {
                AudioManager.instance.SetFightMusicArea("FightMusicValue", 1);
            }
            if (areaType == AreaType.Werewolf)
            {
                AudioManager.instance.SetFightMusicArea("FightMusicValue", 3);
            }
            if (areaType == AreaType.Troll)
            {
                AudioManager.instance.SetFightMusicArea("FightMusicValue", 2);
            }
            initialized = true;
        }
    }
    public void cutSceneStop()
    {
        AudioManager.instance.SetMusicAreaParameter("GameMusic", 1);
        AudioManager.instance.InitializeFightMusic(FMODEvents.instance.music);
        AudioManager.instance.SetFightMusicArea("FightMusicValue", 0);

    }
}
