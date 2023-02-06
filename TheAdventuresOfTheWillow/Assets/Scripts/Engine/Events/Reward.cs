using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    public GameObject RewardPanel;
	public GameObject MensagePanel;
	public int GemValue = 1;
	private bool claimed;
    public float toFalseSeconds = 3f;
	public Timer timer;
	public bool isreadynow;
	
	public void AtiveRewardPanelButton()
	{
		if(isreadynow)
		{
			RewardPanel.SetActive(true);
		    claimed = false;
		}
		
	}
	public void DeactiveRewardPanelButton()
	{
		if(claimed)
		{
		    RewardPanel.SetActive(false);
		}
		else
		{
			MensagePanel.SetActive(true);
            StartCoroutine(ToFalse());
		}
	}
	
	public void GiveGemButton()
	{
		GemCollect.instance.ChangeGem(GemValue);
		claimed = true;
		timer.nointeractable();
		DataPersistenceManager.instance.SaveGame();
		StartCoroutine(ToFalsePanel());
	}
	
	IEnumerator ToFalse()
    {
		yield return new WaitForSeconds(toFalseSeconds);
	    MensagePanel.SetActive(false);
    }
	
	IEnumerator ToFalsePanel()
    {
		yield return new WaitForSeconds(toFalseSeconds);
	    RewardPanel.SetActive(false);
	    MensagePanel.SetActive(false);
    }
}
