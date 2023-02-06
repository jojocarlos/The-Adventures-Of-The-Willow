using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemQuantity : MonoBehaviour, IDataPersistence
{   
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
	
	void Start()
	{
	    DataPersistenceManager.instance.LoadGame(); 
	    AirFlowerAmountTXT.text = AirFlowerQuantity.ToString();
		BubbleFlowerAmountTXT.text = BubbleFlowerQuantity.ToString();
		FireFlowerAmountTXT.text = FireFlowerQuantity.ToString();
		InvincibleFlowerAmountTXT.text = InvincibleFlowerQuantity.ToString();
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