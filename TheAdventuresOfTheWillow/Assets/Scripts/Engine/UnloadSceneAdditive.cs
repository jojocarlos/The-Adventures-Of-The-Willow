using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnloadSceneAdditive : MonoBehaviour
{
    public string lastSceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player" && SceneManager.GetSceneByName(lastSceneName).isLoaded)
        {
            SceneManager.UnloadSceneAsync(lastSceneName);
        }
    }
}
