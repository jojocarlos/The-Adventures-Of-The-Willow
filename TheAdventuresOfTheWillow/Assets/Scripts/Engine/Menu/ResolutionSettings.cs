using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System.IO;

public class ResolutionSettings : MonoBehaviour
{
    public TMPro.TMP_Dropdown ResolutionDropdown;
    public Toggle fullScreenToggle;

    private Resolution[] resolutions;
    private int currentResolutionIndex;
    private bool isFullScreen;

    private const string jsonFileName = "resolutionSettings.json";
    private ResolutionSettingsData resolutionSettingsData = new ResolutionSettingsData();

    [System.Serializable]
    private class ResolutionSettingsData
    {
        public int resolutionIndex;
        public bool isFullScreen;
    }

    private void Awake()
    {
        LoadResolutionSettings();

        ResolutionDropdown.onValueChanged.AddListener(new UnityAction<int>(index =>
        {
            resolutionSettingsData.resolutionIndex = index;
            SaveResolutionSettings();
        }));

        fullScreenToggle.onValueChanged.AddListener(new UnityAction<bool>(value =>
        {
            resolutionSettingsData.isFullScreen = value;
            SaveResolutionSettings();
            SetFullScreen(value);
        }));
    }

    private void Start()
    {
        resolutions = Screen.resolutions;

        ResolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " " + resolutions[i].refreshRate + "Hz";
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height &&
                resolutions[i].refreshRate == Screen.currentResolution.refreshRate)
            {
                currentResolutionIndex = i;
            }
        }

        ResolutionDropdown.AddOptions(options);
        ResolutionDropdown.value = resolutionSettingsData.resolutionIndex;
        ResolutionDropdown.RefreshShownValue();

        SetFullScreen(resolutionSettingsData.isFullScreen);
    }

    private void SetFullScreen(bool value)
    {
        isFullScreen = value;
        Screen.fullScreen = isFullScreen;
    }

    private void SaveResolutionSettings()
    {
        string path = Application.persistentDataPath + "/Configurations";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string json = JsonUtility.ToJson(resolutionSettingsData);
        File.WriteAllText(path + "/" + jsonFileName, json);
    }

    private void LoadResolutionSettings()
    {
        string filePath = Application.persistentDataPath + "/Configurations/" + jsonFileName;
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            resolutionSettingsData = JsonUtility.FromJson<ResolutionSettingsData>(json);
        }
        else
        {
            resolutionSettingsData.resolutionIndex = currentResolutionIndex;
            resolutionSettingsData.isFullScreen = Screen.fullScreen;

            SaveResolutionSettings();
        }
    }
}

