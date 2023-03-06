using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.IO;
using TMPro;
using static KeybindManager;




public class KeybindManager : MonoBehaviour
{
    [System.Serializable]
    public class Keybind
    {
        public string actionName;
        public string inputName;
        public KeyCode keyCode;
    }

    public List<Keybind> keybinds;
    public TextMeshProUGUI[] keybindTexts;
    public GameObject rebindButtonPrefab;
    public Transform rebindButtonContainer;

    private string keybindsFilePath;
    private Dictionary<string, TextMeshProUGUI> keybindTextDict = new Dictionary<string, TextMeshProUGUI>();
    private Dictionary<string, GameObject> rebindButtonDict = new Dictionary<string, GameObject>();
    private GameObject currentRebindButton;
    private string currentActionName;
    private bool isWaitingForKey = false;

    private void Awake()
    {
        keybindsFilePath = Application.persistentDataPath + "/keybinds.json";
        LoadKeybinds();
        UpdateKeybindTexts();
        CreateRebindButtons();
    }

    private void LoadKeybinds()
    {
        if (File.Exists(keybindsFilePath))
        {
            string json = File.ReadAllText(keybindsFilePath);
            keybinds = JsonUtility.FromJson<List<Keybind>>(json);
        }
        else
        {
            // Define os keybinds padrão caso não exista arquivo salvo
            keybinds = new List<Keybind>
            {
                new Keybind { actionName = "Move Up", inputName = "Move", keyCode = KeyCode.W },
                new Keybind { actionName = "Move Down", inputName = "Move", keyCode = KeyCode.S },
                new Keybind { actionName = "Move Left", inputName = "Move", keyCode = KeyCode.A },
                new Keybind { actionName = "Move Right", inputName = "Move", keyCode = KeyCode.D },
                new Keybind { actionName = "Move Up Alt", inputName = "MoveAlt", keyCode = KeyCode.UpArrow },
                new Keybind { actionName = "Move Down Alt", inputName = "MoveAlt", keyCode = KeyCode.DownArrow },
                new Keybind { actionName = "Move Left Alt", inputName = "MoveAlt", keyCode = KeyCode.LeftArrow },
                new Keybind { actionName = "Move Right Alt", inputName = "MoveAlt", keyCode = KeyCode.RightArrow },
                new Keybind { actionName = "Jump", inputName = "Jump", keyCode = KeyCode.Space },
                new Keybind { actionName = "Shoot", inputName = "Shoot", keyCode = KeyCode.Mouse0 },
                new Keybind { actionName = "Dash", inputName = "Dash", keyCode = KeyCode.LeftShift },
                new Keybind { actionName = "Confirm", inputName = "Confirm", keyCode = KeyCode.Return }
            };
        }
    }

    private void SaveKeybinds()
    {
        string json = JsonUtility.ToJson(keybinds);
        File.WriteAllText(keybindsFilePath, json);
    }

    private void UpdateKeybindTexts()
    {
        foreach (TextMeshProUGUI keybindText in keybindTexts)
        {
            string actionName = keybindText.name;
            KeyCode keyCode = GetKeyCode(actionName);
            keybindText.text = string.Format("{0}: {1}", actionName, keyCode.ToString());
            keybindTextDict[actionName] = keybindText;
        }
    }

    private void CreateRebindButtons()
    {
        foreach (Keybind keybind in keybinds)
        {
            string actionName = keybind.actionName;
            KeyCode keyCode = keybind.keyCode;
            GameObject rebindButton = Instantiate(rebindButtonPrefab, rebindButtonContainer);
            rebindButton.GetComponentInChildren<TextMeshProUGUI>().text = actionName;
            rebindButton.GetComponent<Button>().onClick.AddListener(() => StartRebind(actionName));
            rebindButtonDict[actionName] = rebindButton;
        }
    }

    public void StartRebind(string actionName)
    {
        if (isWaitingForKey)
        {
            return;
        }

        currentRebindButton = rebindButtonDict[actionName];
        currentActionName = actionName;
        currentRebindButton.GetComponentInChildren<TextMeshProUGUI>().text = "Press any key...";
        isWaitingForKey = true;
    }
    private void OnGUI()
    {
        if (isWaitingForKey)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                KeyCode newKeyCode = e.keyCode;
                RebindKey(currentActionName, newKeyCode);
                currentRebindButton.GetComponentInChildren<TextMeshProUGUI>().text = currentActionName;
                isWaitingForKey = false;
            }
        }
    }

    public void RebindKey(string actionName, KeyCode newKeyCode)
    {
        // Verifica se a nova tecla já está em uso por outra ação
        foreach (Keybind keybind in keybinds)
        {
            if (keybind.actionName != actionName && keybind.keyCode == newKeyCode)
            {
                Debug.LogErrorFormat("Key {0} already used by action {1}", newKeyCode, keybind.actionName);
                return;
            }
        }

        // Atribui a nova tecla para a ação
        foreach (Keybind keybind in keybinds)
        {
            if (keybind.actionName == actionName)
            {
                keybind.keyCode = newKeyCode;
                SaveKeybinds();
                UpdateKeybindTexts();
                return;
            }
        }
    }

    public KeyCode GetKeyCode(string actionName)
    {
        foreach (Keybind keybind in keybinds)
        {
            if (keybind.actionName == actionName)
            {
                return keybind.keyCode;
            }
        }

        return KeyCode.None;
    }
}