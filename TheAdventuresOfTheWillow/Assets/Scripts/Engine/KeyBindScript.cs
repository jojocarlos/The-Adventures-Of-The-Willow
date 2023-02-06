using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyBindScript : MonoBehaviour
{
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>(); 
	
	public TextMeshProUGUI up, down, right, left, jump, grab, shoot;
	
	private GameObject currentKey;
	
	private Color32 normal = new Color(39, 171, 249, 255);
	private Color32 slected = new Color(239, 116, 36, 255);
	
    void Start()
    {
        keys.Add("Up", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Up", "W")));
		keys.Add("Down", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Down", "S")));
		keys.Add("Left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A")));
		keys.Add("Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D")));
		keys.Add("Jump", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space")));
		keys.Add("Grab", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Grab", "G")));
		keys.Add("Shoot", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Shoot", "T")));
		
		up.text = keys["Up"].ToString();
		down.text = keys["Down"].ToString();
		left.text = keys["Left"].ToString();
		right.text = keys["Right"].ToString();
		jump.text = keys["Jump"].ToString();
		grab.text = keys["Grab"].ToString();
		shoot.text = keys["Shoot"].ToString();
    }

    
    void Update()
    {
        if(Input.GetKeyDown(keys["Up"]))
		{
			Debug.Log("Up");
		}
		if(Input.GetKeyDown(keys["Down"]))
		{
			Debug.Log("Down");
		}
		if(Input.GetKeyDown(keys["Left"]))
		{
			Debug.Log("Left");
		}
		if(Input.GetKeyDown(keys["Right"]))
		{
			Debug.Log("Right");
		}
		if(Input.GetKeyDown(keys["Jump"]))
		{
			Debug.Log("Jump");
		}
		if(Input.GetKeyDown(keys["Grab"]))
		{
			Debug.Log("Grab");
		}
		if(Input.GetKeyDown(keys["Shoot"]))
		{
			Debug.Log("Shoot");
		}
		
    }
	
	
	void OnGUI()
	{			
	    if(currentKey != null)
		{				
	        Event e = Event.current;
	     	if(e.isKey)
     		{
			keys[currentKey.name] = e.keyCode;
			currentKey.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = e.keyCode.ToString();
			currentKey.GetComponent<Image>().color = normal;
			currentKey = null;
			}
		}
	}
	
	public void ChangeKey(GameObject clicked)
	{
		if(currentKey != null)
		{
			currentKey.GetComponent<Image>().color = normal;
		}
		currentKey = clicked;
		currentKey.GetComponent<Image>().color = slected;
	}
	
	public void SaveKeys()
	{
		foreach (var key in keys)
		{
			PlayerPrefs.SetString(key.Key, key.Value.ToString());
		}
		PlayerPrefs.Save();
	}
}
