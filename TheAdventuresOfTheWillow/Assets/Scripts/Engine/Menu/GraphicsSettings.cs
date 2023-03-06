using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System.IO;

public class GraphicsSettings : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Dropdown qualityDropdown;

    private const string jsonFileName = "graphicsSettings.json";

    [System.Serializable]
    private class GraphicsSettingsData
    {
        public int qualityLevel;
    }

    private GraphicsSettingsData graphicsSettingsData = new GraphicsSettingsData();

    private void Awake()
    {
        qualityDropdown.onValueChanged.AddListener(new UnityAction<int>(index =>
        {
            graphicsSettingsData.qualityLevel = qualityDropdown.value;
            SaveGraphicsSettings();
        }));
    }

    private void Start()
    {
        LoadGraphicsSettings();
        qualityDropdown.value = graphicsSettingsData.qualityLevel;
        QualitySettings.SetQualityLevel(graphicsSettingsData.qualityLevel);
    }

    private void SaveGraphicsSettings()
    {
        string path = Application.persistentDataPath + "/Configurations";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        string json = JsonUtility.ToJson(graphicsSettingsData);
        File.WriteAllText(Application.persistentDataPath + "/Configurations/" + jsonFileName, json);
    }

    private void LoadGraphicsSettings()
    {
        string filePath = Application.persistentDataPath + "/Configurations/" + jsonFileName;
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            graphicsSettingsData = JsonUtility.FromJson<GraphicsSettingsData>(json);
        }
        else
        {
            graphicsSettingsData.qualityLevel = 3;
            SaveGraphicsSettings();
        }
    }
}
