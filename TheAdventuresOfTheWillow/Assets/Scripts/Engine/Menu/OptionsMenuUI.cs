using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class OptionsMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject escapeMenu;
    [SerializeField] private GameObject mapMenu;
    [SerializeField] private GameObject volumeObj;

    private bool isPaused;

    void Start()
    {
        isPaused = false;
        escapeMenu.SetActive(false);
        mapMenu.SetActive(false); 
        volumeObj.SetActive(false);
    }

    public void EscapeMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (mapMenu.activeSelf)
            {
                mapMenu.SetActive(false);
                volumeObj.SetActive(false);
                isPaused = false;
                Time.timeScale = 1;
                CursorManager.cursorManagerInstance.cursorDisappear();
            }
            else if (escapeMenu.activeSelf)
            {
                Resume();
            }
            else
            {
                isPaused = true;
                Time.timeScale = 0;
                escapeMenu.SetActive(true);
                volumeObj.SetActive(true);
                CursorManager.cursorManagerInstance.cursorAppear();
            }
        }
    }

    public void MapMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (escapeMenu.activeSelf)
            {
                Resume();
            }
            else
            {
                isPaused = true;
                volumeObj.SetActive(true);
                Time.timeScale = 0;
                mapMenu.SetActive(true);
                CursorManager.cursorManagerInstance.cursorAppear();
            }
        }
    }

    public void Resume()
    {
        isPaused = false;
        volumeObj.SetActive(false);
        Time.timeScale = 1;
        escapeMenu.SetActive(false);
        mapMenu.SetActive(false);
        CursorManager.cursorManagerInstance.cursorDisappear();
    }

}
