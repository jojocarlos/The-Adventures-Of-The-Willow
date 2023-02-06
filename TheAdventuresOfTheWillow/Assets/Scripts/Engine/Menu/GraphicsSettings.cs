using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class GraphicsSettings : MonoBehaviour
{
	
	public TMPro.TMP_Dropdown qualityDropdown;
	
	const string prefName = "Quality";
	
	void Awake()
	{
		qualityDropdown.onValueChanged.AddListener(new UnityAction<int>(index =>
		{
			PlayerPrefs.SetInt(prefName, qualityDropdown.value);
			PlayerPrefs.Save();
		}));
		
	}
	
	void Start()
	{
		qualityDropdown.value = PlayerPrefs.GetInt(prefName, 3);
	}
	
	public void SetQuality(int qualityIndex)
	{
		QualitySettings.SetQualityLevel(qualityIndex);
	}

}
