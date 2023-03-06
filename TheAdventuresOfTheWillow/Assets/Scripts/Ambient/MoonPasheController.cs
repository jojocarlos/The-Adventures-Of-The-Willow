using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonPasheController : MonoBehaviour
{
    public enum TriggerType
    {
        FullMoon,
        WaxingCrescent,
        WaxingGibbous
    }

    [SerializeField] private Animator MoonAnim;
    [SerializeField] private TriggerType triggerType;
    [SerializeField] private bool isFullMoon, isWaxingCrescent, isWaxingGibbous;

    private Collider2D objectCollider;
    private float leftBounds;
    private float rightBounds;

    void Start()
    {
        objectCollider = GetComponent<Collider2D>();
        leftBounds = transform.position.x - objectCollider.bounds.size.x / 2f;
        rightBounds = transform.position.x + objectCollider.bounds.size.x / 2f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            float playerPositionX = other.transform.position.x;

            if (playerPositionX < leftBounds)
            {
                if (triggerType == TriggerType.FullMoon && !isFullMoon)
                {
                    MoonAnim.SetTrigger("FullMoonTrue");
                    isFullMoon = true;
                }
                if (triggerType == TriggerType.WaxingGibbous && !isWaxingGibbous)
                {
                    MoonAnim.SetTrigger("WaxingGibbousTrue");
                    isWaxingGibbous = true;
                }
                if (triggerType == TriggerType.WaxingCrescent && !isWaxingCrescent)
                {
                    MoonAnim.SetTrigger("WaxingCrescentTrue");
                    isWaxingCrescent = true;
                }
            }
            else if (playerPositionX > rightBounds)
            {
                if (triggerType == TriggerType.FullMoon && isFullMoon)
                {
                    MoonAnim.SetTrigger("FullMoonFalse");
                    isFullMoon = false;
                }
                if (triggerType == TriggerType.WaxingGibbous && isWaxingGibbous)
                {
                    MoonAnim.SetTrigger("WaxingGibbousFalse");
                    isWaxingGibbous = false;
                }
                if (triggerType == TriggerType.WaxingCrescent && isWaxingCrescent)
                {
                    MoonAnim.SetTrigger("WaxingCrescentFalse");
                    isWaxingCrescent = false;
                }
            }
           
        }
        
    }
}
