using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    public Texture2D cursor_normal;
	public Vector2 normalCursorHotSpot;
	
	public Texture2D cursor_OnButton;
	public Vector2 OnButtonCursorHotSpot;
	
	   
    public void OnButtonCursorEnter()
    {
        Cursor.SetCursor(cursor_OnButton, OnButtonCursorHotSpot, CursorMode.Auto);
    }
	
	public void OnButtonCursorExit()
    {
        Cursor.SetCursor(cursor_normal, normalCursorHotSpot, CursorMode.Auto);
    }
}
