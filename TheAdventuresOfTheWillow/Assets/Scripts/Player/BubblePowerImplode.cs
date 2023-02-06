using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePowerImplode : MonoBehaviour
{
	public BubblePower bubblepower;
    
	public void bubbleidle()
	{
		bubblepower.implode();
	}
}
