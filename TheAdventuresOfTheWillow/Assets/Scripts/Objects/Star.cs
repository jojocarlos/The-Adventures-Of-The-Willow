using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : Collectable
{
    [SerializeField] int starValue = 1;

    protected override void Collected()
    {
        PowerUps.MyInstance.AddStars(starValue);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.StarCollect, this.transform.position);
        Destroy(this.gameObject);
    }
}
