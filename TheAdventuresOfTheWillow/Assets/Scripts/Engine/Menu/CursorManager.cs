using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
	bool CursorLockedVar;
	
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = (false);
		CursorLockedVar = (false);
    }

    // Update is called once per frame
    void Update()
    {
		if(!CursorLockedVar)
		{
            Cursor.lockState = CursorLockMode.Locked;
		    Cursor.visible = (false);
		}
		
    }
	
	public void cursorAppear()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = (true);
		CursorLockedVar = (true);
	}
	
	public void cursorDisappear()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = (false);
		CursorLockedVar = (false);
	}
}
