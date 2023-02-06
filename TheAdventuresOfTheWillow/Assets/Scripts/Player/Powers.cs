using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powers : MonoBehaviour, IDataPersistence
{
	public static Powers instance;

    public bool isNormal;
	public bool isFirePlayer;
	public bool isBubblePlayer;
	public bool isAirPlayer;
	
	public bool isPowered;
	
	
	public Animator PlayerAnimator;
	
    void Start()
    {
		DataPersistenceManager.instance.LoadGame();
		if(instance == null)
		{
			instance = this;
		}
    }

     
    void Update()
    {
        if(isNormal && !isFirePlayer && !isBubblePlayer && !isAirPlayer)
		{
		    PlayerAnimator.SetLayerWeight(0, 1);
            PlayerAnimator.SetLayerWeight(1, 0);
            PlayerAnimator.SetLayerWeight(2, 0);
            PlayerAnimator.SetLayerWeight(3, 0);
		}
		
		if(!isNormal && isFirePlayer && !isBubblePlayer && !isAirPlayer)
		{
		    PlayerAnimator.SetLayerWeight(0, 0);
            PlayerAnimator.SetLayerWeight(1, 1);
            PlayerAnimator.SetLayerWeight(2, 0);
            PlayerAnimator.SetLayerWeight(3, 0);
		}
		
		if(!isNormal && !isFirePlayer && isBubblePlayer && !isAirPlayer)
		{
		    PlayerAnimator.SetLayerWeight(0, 0);
            PlayerAnimator.SetLayerWeight(1, 0);
            PlayerAnimator.SetLayerWeight(2, 1);
            PlayerAnimator.SetLayerWeight(3, 0);
		}
		
		if(!isNormal && !isFirePlayer && !isBubblePlayer && isAirPlayer)
		{
		    PlayerAnimator.SetLayerWeight(0, 0);
            PlayerAnimator.SetLayerWeight(1, 0);
            PlayerAnimator.SetLayerWeight(2, 0);
            PlayerAnimator.SetLayerWeight(3, 1);
		}
    }
	
	public void isNormalState()
	{
		isNormal = true;
		isFirePlayer = false;
		isBubblePlayer = false;
		isAirPlayer = false;
		isPowered = false;
	}
	
	public void isFirePlayerState()
	{
		isNormal = false;
		isFirePlayer = true;
		isBubblePlayer = false;
		isAirPlayer = false;
		isPowered = true;
	}
	
	public void isBubblePlayerState()
	{
		isNormal = false;
		isFirePlayer = false;
		isBubblePlayer = true;
		isAirPlayer = false;
		isPowered = true;
	}
	
	public void isAirPlayerState()
	{
		isNormal = false;
		isFirePlayer = false;
		isBubblePlayer = false;
		isAirPlayer = true;
		isPowered = true;
	}
	
	public void LoadData(GameData data)
    {
        this.isNormal = data.isNormal;
        this.isFirePlayer = data.isFirePlayer;
        this.isBubblePlayer = data.isBubblePlayer;
		this.isAirPlayer = data.isAirPlayer;
		this.isPowered = data.isPowered;
    }

    public void SaveData(GameData data)
    {
        data.isNormal = this.isNormal;
        data.isFirePlayer = this.isFirePlayer;
        data.isBubblePlayer = this.isBubblePlayer;
        data.isAirPlayer = this.isAirPlayer;
		data.isPowered = this.isPowered;
    }
}
