using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : MonoBehaviour
{
    public static BGMusic instance;
    [SerializeField] private MenuMusicArea area;
    [SerializeField] private MusicArea musicarea;

    [Header("Parameter Change")]
    [SerializeField] private string parameterName;
    [SerializeField] private float parameterValue;
    [Header("Menu Parameter Change")]
    [SerializeField] private string menuParameterName;
    [SerializeField] private float menuParameterValue;
    bool isPlay;

    private void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        if(!isPlay)
        {
            isPlay = true;
            AudioManager.instance.SetMusicMenuArea(area);
            AudioManager.instance.SetMusicArea(musicarea);
        }
    }

    void Update()
    {
        AudioManager.instance.SetMusicAreaParameter(parameterName, parameterValue);
        AudioManager.instance.SetMusicMenuAreaParameter(menuParameterName, menuParameterValue);
    }

    public void destroyObject()
    {
        Destroy(gameObject);
    }
}