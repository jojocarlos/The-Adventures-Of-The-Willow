using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCollect : MonoBehaviour, IDataPersistence
{
    public static CoinCollect instance;
    public TextMeshProUGUI TXTCoins;
    public int coin;
	
    void Start()
    {
		DataPersistenceManager.instance.LoadGame();
        TXTCoins.text = coin.ToString();
        if (instance == null)
        {
            instance = this;
        }

    }
	
	void Update()
	{
		TXTCoins.text = coin.ToString();
	}

    public void ChangeCoin(int coinValue)
    {
        coin += coinValue;
        TXTCoins.text = coin.ToString();
    }

    public void ChangeMinusCoin(int coinValue)
    {
        coin -= coinValue;
        TXTCoins.text = coin.ToString();
    }
     
	public void AddCoin()
	{
		coin += 1;
	}


    public void LoadData(GameData data)
    {
        this.coin = data.coin;
    }

    public void SaveData(GameData data)
    {
        data.coin = this.coin;
    }
}
