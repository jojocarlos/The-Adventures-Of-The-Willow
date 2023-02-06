using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemConsumption : MonoBehaviour, IDataPersistence
{   
    public Powers powers;
	public InvinciblePower invinciblePower;
	
    //AirFlowerItem
    public int AirFlowerQuantity;
	public TextMeshProUGUI AirFlowerAmountTXT;
	
	//BubbleFlowerItem
    public int BubbleFlowerQuantity;
	public TextMeshProUGUI BubbleFlowerAmountTXT;
	
	//FireFlowerItem
	public int FireFlowerQuantity;
	public TextMeshProUGUI FireFlowerAmountTXT;
	
	//InvincibleFlowerItem
	public int InvincibleFlowerQuantity;
	public TextMeshProUGUI InvincibleFlowerAmountTXT;
	
	[SerializeField]private GameObject ispoweredmessage;
	[SerializeField]private GameObject isinvinciblemessage;
	[SerializeField]private GameObject NoMoreItemMessage;
	
	void Start()
	{
	    DataPersistenceManager.instance.LoadGame(); 
	    AirFlowerAmountTXT.text = AirFlowerQuantity.ToString();
		BubbleFlowerAmountTXT.text = BubbleFlowerQuantity.ToString();
		FireFlowerAmountTXT.text = FireFlowerQuantity.ToString();
		InvincibleFlowerAmountTXT.text = InvincibleFlowerQuantity.ToString();
	}
	
	public void AirFlowerConsume()
	{
		if(!powers.isPowered && AirFlowerQuantity >= 1)
		{
			AirFlowerQuantity -= 1;
			powers.isAirPlayerState();
			LoadTextQuantity();
		}
		if(powers.isPowered && AirFlowerQuantity != 0)
		{
			ispoweredmessage.SetActive(true);
		}
		if(AirFlowerQuantity == 0)
	    {
			NoMoreItemMessage.SetActive(true);
		}
	}
	
	public void FireFlowerConsume()
	{
		if(!powers.isPowered && FireFlowerQuantity >= 1)
		{
			FireFlowerQuantity -= 1;
			powers.isFirePlayerState();
			LoadTextQuantity();
		}
		if(powers.isPowered && FireFlowerQuantity != 0)
		{
			ispoweredmessage.SetActive(true);
		}
		
		if(FireFlowerQuantity == 0)
	    {
			NoMoreItemMessage.SetActive(true);
		}
	}
	
	public void BubbleFlowerConsume()
	{
		if(!powers.isPowered && BubbleFlowerQuantity >= 1)
		{
			BubbleFlowerQuantity -= 1;
			powers.isBubblePlayerState();
			LoadTextQuantity();
		}
		if(powers.isPowered && BubbleFlowerQuantity != 0)
		{
			ispoweredmessage.SetActive(true);
		}
		if(BubbleFlowerQuantity == 0)
	    {
			NoMoreItemMessage.SetActive(true);
		}
	}
	
	public void InvincibleFlowerConsume()
	{
		if(!invinciblePower.isInvincible && InvincibleFlowerQuantity >= 1)
		{
			InvincibleFlowerQuantity -= 1;
			invinciblePower.isInvincible = true;
			LoadTextQuantity();
		}
		if(invinciblePower.isInvincible && InvincibleFlowerQuantity != 0)
		{
			isinvinciblemessage.SetActive(true);
		}
		if(InvincibleFlowerQuantity == 0)
	    {
			NoMoreItemMessage.SetActive(true);
		}
	}
	
	void Update()
	{
		
	    AirFlowerAmountTXT.text = AirFlowerQuantity.ToString();
		BubbleFlowerAmountTXT.text = BubbleFlowerQuantity.ToString();
		FireFlowerAmountTXT.text = FireFlowerQuantity.ToString();
		InvincibleFlowerAmountTXT.text = InvincibleFlowerQuantity.ToString();
	}
	
	public void LoadTextQuantity()
	{
	    AirFlowerAmountTXT.text = AirFlowerQuantity.ToString();
		BubbleFlowerAmountTXT.text = BubbleFlowerQuantity.ToString();
		FireFlowerAmountTXT.text = FireFlowerQuantity.ToString();
		InvincibleFlowerAmountTXT.text = InvincibleFlowerQuantity.ToString();
		DataPersistenceManager.instance.SaveGame();
	}

    public void LoadData(GameData data)
    {
        this.AirFlowerQuantity = data.AirFlowerQuantity;
		this.BubbleFlowerQuantity = data.BubbleFlowerQuantity;
		this.FireFlowerQuantity = data.FireFlowerQuantity;
		this.InvincibleFlowerQuantity = data.InvincibleFlowerQuantity;
    }

    public void SaveData(GameData data)
    {
        data.AirFlowerQuantity = this.AirFlowerQuantity;
		data.BubbleFlowerQuantity = this.BubbleFlowerQuantity;
		data.FireFlowerQuantity = this.FireFlowerQuantity;
		data.InvincibleFlowerQuantity = this.InvincibleFlowerQuantity;
    }

}