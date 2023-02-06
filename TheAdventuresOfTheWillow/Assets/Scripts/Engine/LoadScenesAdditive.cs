using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenesAdditive : MonoBehaviour
{
    public string nextSceneName;
    public static bool load;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !SceneManager.GetSceneByName(nextSceneName).isLoaded)
        {
            if(load)
            {
                load = false;
                return;
            }
            SceneManager.LoadScene(nextSceneName, LoadSceneMode.Additive);
            load = true;
        }
    }
}
