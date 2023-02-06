using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCursor : MonoBehaviour
{
	public CursorManager cursorManager;
	
    
    void Update()
    {
        cursorManager.cursorAppear();
    }
}
