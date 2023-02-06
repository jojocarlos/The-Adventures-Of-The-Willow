using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashWait : MonoBehaviour
{
    public float wait_time = 5f;
	public string sceneNameAsync;
	
    void Start()
    {
        StartCoroutine(wait_for_splash(sceneNameAsync));
    }

    IEnumerator wait_for_splash(string sceneNameAsync)
	{
		yield return new WaitForSeconds(wait_time);
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneNameAsync);
	}
}
