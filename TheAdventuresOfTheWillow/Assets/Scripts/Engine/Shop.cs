using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    CoinCollect coinCollect;
    public GameObject NoCoinMessage;
	public ItemQuantity itemQuantityScript;
	public GameObject MaxItemMessage;
	
    //AirFlowerItem
	public int AirFlowerPrice;
	public TextMeshProUGUI AirFlowerPriceTXT;
	
	//BubbleFlowerQuantity
	public int BubbleFlowerPrice;
	public TextMeshProUGUI BubbleFlowerPriceTXT;
	
	//AirFlowerItem
	public int FireFlowerPrice;
	public TextMeshProUGUI FireFlowerPriceTXT;
	
	//AirFlowerItem
	public int InvincibleFlowerPrice;
	public TextMeshProUGUI InvincibleFlowerPriceTXT;
	
	void Start()
	{
		AirFlowerPriceTXT.text = AirFlowerPrice.ToString();
		BubbleFlowerPriceTXT.text = BubbleFlowerPrice.ToString();
		FireFlowerPriceTXT.text = FireFlowerPrice.ToString();
		InvincibleFlowerPriceTXT.text = InvincibleFlowerPrice.ToString();
	}
	
	public void BuyAirFlower()
	{
	    if(CoinCollect.instance.coin >= AirFlowerPrice && itemQuantityScript.AirFlowerQuantity <=5 && itemQuantityScript.AirFlowerQuantity != 5)
		{
		    CoinCollect.instance.ChangeMinusCoin(AirFlowerPrice);
			itemQuantityScript.AirFlowerQuantity += 1;
			itemQuantityScript.LoadTextQuantity();
		}
		if(CoinCollect.instance.coin <= AirFlowerPrice && itemQuantityScript.AirFlowerQuantity != 5)
		{
		    NoCoinMessage.SetActive(true);
		}
		if(itemQuantityScript.AirFlowerQuantity == 5)
		{
			MaxItemMessage.SetActive(true);
		}
	}
	
	public void BuyBubbleFlower()
	{
	    if(CoinCollect.instance.coin >= BubbleFlowerPrice && itemQuantityScript.BubbleFlowerQuantity <= 5 && itemQuantityScript.BubbleFlowerQuantity != 5)
		{
		    CoinCollect.instance.ChangeMinusCoin(BubbleFlowerPrice);
			itemQuantityScript.BubbleFlowerQuantity += 1;
		    itemQuantityScript.LoadTextQuantity();
		}
		if(CoinCollect.instance.coin <= BubbleFlowerPrice && itemQuantityScript.BubbleFlowerQuantity != 5)
		{
		    NoCoinMessage.SetActive(true);
		}
		if(itemQuantityScript.BubbleFlowerQuantity == 5)
		{
			MaxItemMessage.SetActive(true);
		}
	}
	
	public void BuyFireFlower()
	{
	    if(CoinCollect.instance.coin >= FireFlowerPrice && itemQuantityScript.FireFlowerQuantity <= 5 && itemQuantityScript.FireFlowerQuantity != 5)
		{
		    CoinCollect.instance.ChangeMinusCoin(FireFlowerPrice);
			itemQuantityScript.FireFlowerQuantity += 1;
		    itemQuantityScript.LoadTextQuantity();
		}
		if(CoinCollect.instance.coin <= FireFlowerPrice && itemQuantityScript.FireFlowerQuantity != 5)
		{
		    NoCoinMessage.SetActive(true);
		}
		if(itemQuantityScript.FireFlowerQuantity == 5)
		{
			MaxItemMessage.SetActive(true);
		}
	}
	
	public void BuyInvincibleFlower()
	{
	    if(CoinCollect.instance.coin >= InvincibleFlowerPrice && itemQuantityScript.InvincibleFlowerQuantity <= 5 && itemQuantityScript.InvincibleFlowerQuantity != 5)
		{
		    CoinCollect.instance.ChangeMinusCoin(InvincibleFlowerPrice);
			itemQuantityScript.InvincibleFlowerQuantity += 1;
		    itemQuantityScript.LoadTextQuantity();
		}
		if(CoinCollect.instance.coin <= InvincibleFlowerPrice && itemQuantityScript.InvincibleFlowerQuantity != 5)
		{
		    NoCoinMessage.SetActive(true);
		}
		if(itemQuantityScript.InvincibleFlowerQuantity == 5)
		{
			MaxItemMessage.SetActive(true);
		}
	}

}