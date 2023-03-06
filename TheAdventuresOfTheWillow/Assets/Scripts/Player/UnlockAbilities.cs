using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockAbilities : MonoBehaviour
{
    public enum UnlockAbilitieType
    {
        unlockSwim,
        unlockDash,
        increaseCoyoteTime,
        increaseCoyoteTimeBuffer
    }

    [SerializeField] private UnlockAbilitieType uAbilitieType;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(uAbilitieType == UnlockAbilitieType.unlockDash)
            {
                PlayerAbilities.playerAbilitiesInstance.UnlockAbility(PlayerAbilities.AbilityType.Dash);
            }
            if (uAbilitieType == UnlockAbilitieType.unlockSwim)
            {
                PlayerAbilities.playerAbilitiesInstance.UnlockAbility(PlayerAbilities.AbilityType.Swimming);
            }
            if (uAbilitieType == UnlockAbilitieType.increaseCoyoteTime)
            {
                PlayerAbilities.playerAbilitiesInstance.UnlockAbility(PlayerAbilities.AbilityType.coyoteTimeIncrease);
            }
            if (uAbilitieType == UnlockAbilitieType.increaseCoyoteTimeBuffer)
            {
                PlayerAbilities.playerAbilitiesInstance.UnlockAbility(PlayerAbilities.AbilityType.coyoteTimeBufferIncrease);
            }
            Destroy(gameObject);
        }
    }
}
