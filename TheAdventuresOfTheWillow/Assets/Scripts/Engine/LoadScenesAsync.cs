using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoadScenesAsync : MonoBehaviour
{
	
	public GameObject loadScreen;
	public Slider slider;
    public TextMeshProUGUI progressText;
	
    public void LoadSceneAsync(string sceneNameAsync)
	{
		StartCoroutine(LoadAsynchronously(sceneNameAsync));
	}
	
	IEnumerator LoadAsynchronously (string sceneNameAsync)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneNameAsync);
		loadScreen.SetActive(true);
		
		while (!operation.isDone)
		{
			float progress = Mathf.Clamp01(operation.progress / .9f);
			slider.value = progress;
			progressText.text = progress * 100f + "%";
			yield return null;
		}
	}
}
