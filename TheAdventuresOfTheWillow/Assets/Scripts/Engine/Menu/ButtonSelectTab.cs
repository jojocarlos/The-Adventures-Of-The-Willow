using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelectTab : MonoBehaviour
{
	public Animator BT2;
	public Animator BT3;
	public Animator BT1;
	public Animator BT4;
	
	public GameObject panelwarming;
	
    public void ButtonSelected()
	{
		BT2.SetTrigger("Change");
		BT3.SetTrigger("Change");
		BT1.SetTrigger("Change");
		BT4.SetTrigger("Change");
	}
	
	public void disablepanelwarming()
	{
		panelwarming.SetActive(false);
	}
	public void enablepanelwarming()
	{
		panelwarming.SetActive(true);
	}
}
