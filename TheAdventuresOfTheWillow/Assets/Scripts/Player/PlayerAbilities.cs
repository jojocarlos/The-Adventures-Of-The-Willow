using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour, IDataPersistence
{
    public static PlayerAbilities playerAbilitiesInstance;
    public PlayerData Data;
    public enum AbilityType
    {
        Dash,
        Swimming,
        coyoteTimeIncrease,
        coyoteTimeBufferIncrease
    }

    public bool hasSwimming = false;

    //coyoteTime 
    [SerializeField] float coyoteTimeLevel = 0f;
    [SerializeField] private int itemCounter = 0; 
    [SerializeField] private float coyoteTimeIncrease = 0.1f;

    //coyoteTimeBuffer 0.2 default
    [SerializeField] float coyoteTimeBufferLevel = 0.2f;
    [SerializeField] private int itemBufferCounter = 0;
    [SerializeField] private float coyoteTimeBufferIncrease = 0.1f;

    //DashAmout
    [SerializeField] int dashAmountLevel = 0;
    [SerializeField] int itemDashCounter = 0;
    [SerializeField] int dashAmountIncrease = 1;

    private void Start()
    {
        if(playerAbilitiesInstance == null)
        {
            playerAbilitiesInstance = this;
        }
        DataPersistenceManager.instance.LoadGame();
        Data.dashAmount = dashAmountLevel;
        Data.coyoteTime = coyoteTimeLevel;
        Data.jumpInputBufferTime = coyoteTimeBufferLevel;
    }


    public void UnlockAbility(AbilityType ability)
    {
        switch (ability)
        {
            case AbilityType.Dash:
                if (itemDashCounter < 3 && Data.dashAmount < 3)//increase 0 to 3
                {
                    dashAmountLevel += dashAmountIncrease;
                    Data.dashAmount = dashAmountLevel;
                    itemDashCounter++;
                }
                break;
            case AbilityType.Swimming:
                hasSwimming = true;
                break;
            case AbilityType.coyoteTimeIncrease:
                if (itemCounter < 5 && Data.coyoteTime <= 0.5)//increase 0 to 0.5
                {
                    coyoteTimeLevel += coyoteTimeIncrease;
                    Data.coyoteTime = coyoteTimeLevel;
                    itemCounter++;
                }
                break;
            case AbilityType.coyoteTimeBufferIncrease:
                if (itemBufferCounter < 3 && Data.jumpInputBufferTime <= 0.5)//increase 0.2 to 0.5
                {
                    coyoteTimeBufferLevel += coyoteTimeBufferIncrease;
                    Data.jumpInputBufferTime = coyoteTimeBufferLevel;
                    itemCounter++;
                }
                break;
            default:
                Debug.LogError("Invalid ability type.");
                break;
        }

    }

    public void LoadData(GameData data)
    {
        this.hasSwimming = data.hasSwimming;
        this.coyoteTimeLevel = data.coyoteTimeLevel;
        this.coyoteTimeBufferLevel = data.coyoteTimeBufferLevel;
        this.dashAmountLevel = data.dashAmountLevel;
    }

    public void SaveData(GameData data)
    {
        data.hasSwimming = this.hasSwimming;
        data.coyoteTimeLevel = this.coyoteTimeLevel;
        data.coyoteTimeBufferLevel = this.coyoteTimeBufferLevel;
        data.dashAmountLevel = this.dashAmountLevel;
    }
}
