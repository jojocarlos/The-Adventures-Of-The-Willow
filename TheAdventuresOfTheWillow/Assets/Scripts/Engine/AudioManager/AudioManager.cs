using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.IO;
using UnityEngine.Rendering;

[System.Serializable]
public class AudioSettingsData
{
    public float masterVolume;
    public float musicVolume;
    public float ambienceVolume;
    public float sfxVolume;
}

public class AudioManager : MonoBehaviour
{
    [Header("Volume")]
    [Range(0, 1)]
    public float masterVolume = 1;
    [Range(0, 1)]
    public float musicVolume = 1;
    [Range(0, 1)]
    public float ambienceVolume = 1;
    [Range(0, 1)]
    public float SFXVolume = 1;

    private Bus masterBus;
    private Bus musicBus;
    private Bus ambienceBus;
    private Bus sfxBus;

    private List<EventInstance> eventInstances;
    private List<StudioEventEmitter> eventEmitters;

    private EventInstance ambienceEventInstance;
    private EventInstance musicEventInstance;
    private EventInstance FightMusicEventInstance;
    private EventInstance menuMusicEventInstance;

    private const string jsonFileName = "audioSettings.json";
    private AudioSettingsData audioSettingsData = new AudioSettingsData();

    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Audio Manager in the scene.");
        }
        instance = this;


        eventInstances = new List<EventInstance>();
        eventEmitters = new List<StudioEventEmitter>();

        masterBus = RuntimeManager.GetBus("bus:/");
        musicBus = RuntimeManager.GetBus("bus:/Music");
        ambienceBus = RuntimeManager.GetBus("bus:/Ambience");
        sfxBus = RuntimeManager.GetBus("bus:/SFX");

        masterBus.setVolume(masterVolume);
        musicBus.setVolume(musicVolume);
        ambienceBus.setVolume(ambienceVolume);
        sfxBus.setVolume(SFXVolume);

        LoadSettings();
    }

    private void Start()
    {
        InitializeAmbience(FMODEvents.instance.ambience);
    }

    private void Update()
    {
        masterBus.setVolume(masterVolume);
        musicBus.setVolume(musicVolume);
        ambienceBus.setVolume(ambienceVolume);
        sfxBus.setVolume(SFXVolume);
    }
    //Start ambience, music, FightMusic, MenuMusic
    public void InitializeAmbience(EventReference ambienceEventReference)
    {
        ambienceEventInstance = CreateInstance(ambienceEventReference);
        ambienceEventInstance.start();
    }
    
    public void InitializeMusic(EventReference musicEventReference)
    {
        musicEventInstance = CreateInstance(musicEventReference);
        musicEventInstance.start();
    }
    public void InitializeFightMusic(EventReference FightMusicEventReference)
    {
        FightMusicEventInstance = CreateInstance(FightMusicEventReference);
        FightMusicEventInstance.start();
    }

    public void InitializeMenuMusic(EventReference menuMusicEventReference)
    {
        menuMusicEventInstance = CreateInstance(menuMusicEventReference);
        menuMusicEventInstance.start();
    }

    //Stop ambience, music, FightMusic, MenuMusic
    public void StopAmbience(EventReference ambienceEventReference)
    {
        ambienceEventInstance = CreateInstance(ambienceEventReference);
        ambienceEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public void StopMusic(EventReference musicEventReference)
    {
        musicEventInstance = CreateInstance(musicEventReference);
        musicEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
    public void StopFightMusic(EventReference FightMusicEventReference)
    {
        FightMusicEventInstance = CreateInstance(FightMusicEventReference);
        FightMusicEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public void StopMenuMusic(EventReference menuMusicEventReference)
    {
        menuMusicEventInstance = CreateInstance(menuMusicEventReference);
        menuMusicEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }


    //ambience parameter name and value
    public void SetAmbienceParameter(string parameterName, float parameterValue)
    {
        ambienceEventInstance.setParameterByName(parameterName, parameterValue);
    }


    //music name to area
    public void SetMusicArea(MusicArea area)
    {
        musicEventInstance.setParameterByName("area", (float) area);
    }
    //music parameter name and value
    public void SetMusicAreaParameter(string parameterName, float parameterValue)
    {
        musicEventInstance.setParameterByName(parameterName, parameterValue);
    }


    //menu music
    public void SetMusicMenuArea(MenuMusicArea MenuMusicChange)
    {
        musicEventInstance.setParameterByName("MusicMenuChange", (float)MenuMusicChange);
    }
    //menu music parameter name and value
    public void SetMusicMenuAreaParameter(string parameterName, float parameterValue)
    {
        menuMusicEventInstance.setParameterByName(parameterName, parameterValue);
    }


    //music name to area
    public void SetFightMusicArea(FightMusicArea area)
    {
        FightMusicEventInstance.setParameterByName("area", (float)area);
    }

    //Fight music paramater name and value
    public void SetFightMusicArea(string parameterName, float parameterValue)
    {
        FightMusicEventInstance.setParameterByName(parameterName, parameterValue);
    }




    //oneshot 3D pos
    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }
    //oneshot normal
    public void PlayOneShot(EventReference sound)
    {
        RuntimeManager.PlayOneShot(sound);
    }


    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    public StudioEventEmitter InitializeEventEmitter(EventReference eventReference, GameObject emitterGameObject)
    {
        StudioEventEmitter emitter = emitterGameObject.GetComponent<StudioEventEmitter>();
        emitter.EventReference = eventReference;
        eventEmitters.Add(emitter);
        return emitter;
    }

    private void CleanUp()
    {
        // stop and release any created instances
        foreach (EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
        // stop all of the event emitters, because if we don't they may hang around in other scenes
        foreach (StudioEventEmitter emitter in eventEmitters)
        {
            emitter.Stop();
        }
    }

    private void OnDestroy()
    {
        CleanUp();
    }

    public void SaveSettings()
    {
        audioSettingsData.masterVolume = masterVolume;
        audioSettingsData.musicVolume = musicVolume;
        audioSettingsData.ambienceVolume = ambienceVolume;
        audioSettingsData.sfxVolume = SFXVolume;

        string path = Application.persistentDataPath + "/Configurations";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string json = JsonUtility.ToJson(audioSettingsData);
        File.WriteAllText(path + "/" + jsonFileName, json);
    }

    public void LoadSettings()
    {

        string filePath = Application.persistentDataPath + "/Configurations/" + jsonFileName;
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            audioSettingsData = JsonUtility.FromJson<AudioSettingsData>(json);

            masterVolume = audioSettingsData.masterVolume;
            musicVolume = audioSettingsData.musicVolume;
            ambienceVolume = audioSettingsData.ambienceVolume;
            SFXVolume = audioSettingsData.sfxVolume;
        }
        else
        {
            SaveSettings();
        }
    }

}
