using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSettings : MonoBehaviour
{
	public GameObject PanelReset;
	public GameObject PanelConfirmation;
	
    
    public void resetSetting()
    {
        PanelReset.SetActive(false);
		PanelConfirmation.SetActive(true);
    }

    
    public void yes()
    {
        PlayerPrefs.DeleteAll();
    }
	
	public void no()
    {
        PanelConfirmation.SetActive(false);
		PanelReset.SetActive(true);
    }
}
