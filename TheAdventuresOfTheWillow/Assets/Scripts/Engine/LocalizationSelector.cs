using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalizationSelector : MonoBehaviour, IDataPersistence
{
	public int intID = 0;
	
    private void Start()
	{
		DataPersistenceManager.instance.LoadGame(); 
		int ID = intID;
	    //int ID = PlayerPrefs.GetInt("LocaleKey", 0);
		ChangeLocale(ID);
	}
    private bool active = false;
	
    public void ChangeLocale(int localeID)
	{ 
	    if(active == true)
		   return;
		StartCoroutine(SetLocale(localeID));
	}

    IEnumerator SetLocale(int _localeID)
    {
	    active = true;
	    yield return LocalizationSettings.InitializationOperation;
		LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
	    //PlayerPrefs.SetInt("LocaleKey", _localeID);
		intID = _localeID;
		active = false;
		DataPersistenceManager.instance.SaveGame(); 
	}
	
	 public void LoadData(GameData data)
    {
        this.intID = data.intID;
        
    }

    public void SaveData(GameData data)
    {
        data.intID = this.intID;
    }
}
