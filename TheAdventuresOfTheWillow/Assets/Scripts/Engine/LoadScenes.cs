using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class LoadScenes : MonoBehaviour
{
	public string nametoescape;
	
    public void loadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void loadSceneAdditive(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Additive);
    }

    public void QuitGame()
	{
		Application.Quit();
	}
	
	public void EscapeToBack(InputAction.CallbackContext context)
	{
        if (context.performed)
     	{
			loadSceneEscape(nametoescape);
		}
    }
	/*/old
	void Update()
	{
		if (Input.GetKeyDown("escape"))
		{
			loadSceneEscape(name);
		}
	}*/
	
	public void loadSceneEscape(string nametoescape)
    {
        SceneManager.LoadScene(nametoescape);
    }
	
}
