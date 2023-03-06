using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField] private Button continueGameButton;
    [SerializeField] private Button loadGameButton;

    // Update is called once per frame
    void Update()
    {
        if (!DataPersistenceManager.instance.HasGameData())
        {
            continueGameButton.interactable = false;
            loadGameButton.interactable = false;
            Debug.Log("no data");
        }
    }
}
