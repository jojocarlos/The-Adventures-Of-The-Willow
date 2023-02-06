using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : MonoBehaviour
{
    public static BGMusic instance;
    [SerializeField] private MenuMusicChanger MenuMusicChange;

    void Start()
    {
        AudioManager.instance.SetMusicMenuArea(MenuMusicChange);
    }

    public void destroyObject()
    {
        Destroy(this);
    }
}