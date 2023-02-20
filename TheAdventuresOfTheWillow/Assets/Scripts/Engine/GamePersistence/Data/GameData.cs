using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData
{
    public long lastUpdated;

    public int currentHealth;
    public int coin;
	public int Gem;
    public bool Bought1;
    public bool Bought2;
    public bool Bought3;
	public bool isNormal;
	public bool isFirePlayer;
	public bool isBubblePlayer;
	public bool isAirPlayer;
	public bool isPowered;
    public int releasedLevelStatic;
	public int intID;
	public int deathCount;

    //Player Position worlds
    public bool isWorld1;
    public bool isWorld2;
    public Vector3 playerPosInWorld1;
    public Vector3 playerPosInWorld2;

    //Player position
    public Vector3 playerPosition;

    //Shop Items
    public int AirFlowerQuantity;
	public int BubbleFlowerQuantity;
	public int FireFlowerQuantity;
	public int InvincibleFlowerQuantity;

    public SerializableDictionary<string, bool> coinsCollected;
    public SerializableDictionary<string, bool> doorsOpened;
    public SerializableDictionary<string, bool> keyDoorOpened;
    public SerializableDictionary<string, Vector3> ObjectsPosition;
    public SerializableDictionary<string, bool> ObjectsPositionBool;

    //public AttributesData playerAttributesData;

    public GameData()
    {
		//Localization ID
		this.intID = 0;
		
        //GamePlayerData
		this.deathCount = 0;
        this.coin = 0;
		this.Gem = 0;
        this.currentHealth = 100;

        //Player position World
        playerPosInWorld1 = Vector3.zero;
        playerPosInWorld2 = Vector3.zero;
        this.isWorld1 = true;
        this.isWorld2 = false;
        playerPosition = Vector3.zero;

        //PlayerStates
        this.isNormal = true;
		this.isFirePlayer = false;
		this.isBubblePlayer = false;
		this.isAirPlayer = false;
		this.isPowered = false;
		
        coinsCollected = new SerializableDictionary<string, bool>();
        doorsOpened = new SerializableDictionary<string, bool>();
        keyDoorOpened = new SerializableDictionary<string, bool>();
        ObjectsPosition = new SerializableDictionary<string, Vector3>();
        ObjectsPositionBool = new SerializableDictionary<string, bool>();
        //playerAttributesData = new AttributesData();
        //WorldShop
        this.Bought1 = true;
        this.Bought2 = false;
        this.Bought3 = false;

        //Levels
        this.releasedLevelStatic = 1;
		
		//Shop items quantity
		this.AirFlowerQuantity = 0;
		this.BubbleFlowerQuantity = 0;
		this.FireFlowerQuantity = 0;
		this.InvincibleFlowerQuantity = 0;

    }

    /*/
    public int GetPercentageComplete()
    {
        //figure out how many coins we've collected
        int totalCollected = 0;
        foreach (bool collected in coinsCollected.Values)
        {
            if (collected)
            {
                totalCollected++;
            }
        }

        // ensure we don't divide by 0 when calculating the percentage
        int percentageCompleted = -1;
        if(coinsCollected.Count != 0)
        {
            percentageCompleted = (totalCollected * 100 / coinsCollected.Count);
        }
        return percentageCompleted;
    }
    */
}
