using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ResetSettings : MonoBehaviour
{
    public GameObject PanelReset;
    public GameObject PanelConfirmation;

    private const string resolutionFileName = "resolutionSettings.json";
    private const string graphicsFileName = "graphicsSettings.json";

    public void resetSetting()
    {
        PanelReset.SetActive(false);
        PanelConfirmation.SetActive(true);
    }

    public void yes()
    {

        PlayerPrefs.DeleteAll();
        DeleteGraphicsSettings();
        DeleteResolutionSettings();
        DeleteAudioSettings();
    }
    private void DeleteResolutionSettings()
    {
        string filePath = Application.persistentDataPath + "/Configurations/" + "resolutionSettings.json";
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    private void DeleteGraphicsSettings()
    {
        string filePath = Application.persistentDataPath + "/Configurations/" + "graphicsSettings.json";
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
    private void DeleteAudioSettings()
    {
        string filePath = Application.persistentDataPath + "/Configurations/" + "audioSettings.json";
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    public void no()
    {
        PanelConfirmation.SetActive(false);
        PanelReset.SetActive(true);
    }
}
