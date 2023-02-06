using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class ResolutionSettings : MonoBehaviour
{
    public TMPro.TMP_Dropdown ResolutionDropdown;

    Resolution[] resolutions;
	const string resName = "resolutionoption";
	
	public Toggle fullScreenToggle;
	private int screenInt;
    private bool isFullScreen;
	
    void Awake()
	{
		screenInt = PlayerPrefs.GetInt("FullScreen");
		
		if(screenInt == 1)
		{
			fullScreenToggle.isOn = true;
		}
		if(screenInt == 0)
		{
			fullScreenToggle.isOn = false;
		}
		
		
		ResolutionDropdown.onValueChanged.AddListener(new UnityAction<int>(index =>
		{
			PlayerPrefs.SetInt(resName, ResolutionDropdown.value);
			PlayerPrefs.Save();
		}));
	}
	
	public void SetFullScreen(bool isFullScreen)
	{
		if(fullScreenToggle.isOn)
		{
			PlayerPrefs.SetInt("FullScreen", 1);
			isFullScreen = true;
		}
		if(!fullScreenToggle.isOn)
		{
			PlayerPrefs.SetInt("FullScreen", 0);
			isFullScreen = false;
		}
		Screen.fullScreen = isFullScreen;
    }
	
    void Start()
    {
		
        resolutions = Screen.resolutions;
        ResolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
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
        ResolutionDropdown.value = PlayerPrefs.GetInt(resName, currentResolutionIndex);
        ResolutionDropdown.RefreshShownValue();
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
	
}
