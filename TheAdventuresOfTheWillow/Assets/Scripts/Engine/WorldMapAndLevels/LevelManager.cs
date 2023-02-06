using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int level;
    public SpriteRenderer Lock;
	public bool islocked;

    public string levelString;
    public LevelButton levelButton;
	
	//Load Level AsyncOperation
	public GameObject loadScreen;
	public Slider slider;
    public TextMeshProUGUI progressText;
	public string sceneNameAsync;
	
	void Update()
	{
	}
	public void SceneToLoad()
	{
		progressText.text = "0%";
		slider.value = 0;
		StartCoroutine(LoadAsynchronously(sceneNameAsync));
		loadScreen.SetActive(true); 
	}
	//not used
	public void LoadSceneAsync(string sceneNameAsync)
	{
		progressText.text = "0%";
		slider.value = 0;
		StartCoroutine(LoadAsynchronously(sceneNameAsync));
	}
	
	IEnumerator LoadAsynchronously (string sceneNameAsync)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneNameAsync);
		
		while (!operation.isDone)
		{
			float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
			slider.value = progressValue;
			progressText.text = progressValue * 100f + "%";
			yield return null;
		}
		
	}
	

    void Start()
    {
		loadScreen.SetActive(false);
        if(levelButton.releasedLevelStatic >= level)
        {
            LevelUnlocked();
        }
        else
        {
            LevelLocked();
        }
    }
	//level load test but not used
    public void LevelSelect(string _level)
    {
        levelString = _level;
        SceneManager.LoadScene(levelString);
    }
    void LevelLocked()
    {
		islocked = true;
        Lock.enabled = true;
    }
    void LevelUnlocked()
    {
		islocked = false;
        Lock.enabled = false;
    }



}
