using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MapMenu : MonoBehaviour
{
    [SerializeField] private GameObject MapMenuObj;
    [SerializeField] private bool isPausedMap;
    public CursorManager cursorManager;


    public void MapMenuPress(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isPausedMap = !isPausedMap;
        }
    }

    private void Update()
    {
        /*/
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }*/
        if (isPausedMap)
        {
            ActivateMapMenu();
        }
        else
        {
            DeactivateMapMenu();
        }
    }

    public void ActivateMapMenu()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        MapMenuObj.SetActive(true);
        cursorManager.cursorAppear();
    }
    public void DeactivateMapMenu()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        MapMenuObj.SetActive(false);
        isPausedMap = false;
        cursorManager.cursorDisappear();
    }

}
