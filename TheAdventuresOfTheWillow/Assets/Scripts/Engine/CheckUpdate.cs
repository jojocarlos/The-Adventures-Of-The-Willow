using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class CheckUpdate : MonoBehaviour
{
    public string versionUrl = "";
	public string currentVersion;
	private string latestVersion;
	public GameObject newVersionAvailable;
	public TextMeshProUGUI TXTCurrentVersion;
	
    void Start()
    {
        StartCoroutine(LoadTxtData(versionUrl));
	    TXTCurrentVersion.text = currentVersion.ToString();
    }
	
	private IEnumerator LoadTxtData(string url)
	{
		UnityWebRequest loaded = new UnityWebRequest(url);
		loaded.downloadHandler = new DownloadHandlerBuffer();
		yield return loaded.SendWebRequest();
		
		latestVersion = loaded.downloadHandler.text;
		CheckVersion();
	}
	
	private void CheckVersion()
	{
		Debug.Log("currentVersion=" + currentVersion);
		Debug.Log("latestVersion=" + latestVersion);
		
		Version versionDevice = new Version(currentVersion);
		Version versionServer = new Version(latestVersion);
		int result = versionDevice.CompareTo(versionServer);
		
		if((latestVersion != "") && (result < 0))
		   newVersionAvailable.SetActive(true);
		
	}
	
	public void ClosePopUp(GameObject obj)
	{
		obj.SetActive(false);
	}
	
	public void OpenURL(string url)
	{
		Application.OpenURL(url);
	}

}
