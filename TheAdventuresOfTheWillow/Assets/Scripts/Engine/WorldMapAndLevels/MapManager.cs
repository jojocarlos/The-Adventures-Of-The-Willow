using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class MapManager : MonoBehaviour
{
	public Character Character;
	public Pin StartPin;
	public TextMeshProUGUI SelectedLevelText;
	
	//Movement
    [Header("Walk System")]
    public float moveSpeed = 20f;
    private float move;
    private float vertical;
	
	public float timetoload = 1f;
	public Animator Anim;
    bool facingRight = true;
	public GameObject playerCharacterToFlip;
	bool isUp;
	
	public ScreenAspectRatio screenAspectRatio;
	
	/// <summary>
	/// Use this for initialization
	/// </summary>
	private void Start ()
	{
		// Pass a ref and default the player Starting Pin
		Character.Initialise(this, StartPin);
	}


	/// <summary>
	/// This runs once a frame
	/// </summary>
	private void Update()
	{
		// Only check input when character is stopped
		if (Character.IsMoving) return;
		
		// First thing to do is try get the player input
		CheckForInput();
		
		if(move == 0)
		{
			Anim.SetBool("Walk", false);
			Anim.SetBool("WalkFront", false);
			Anim.SetBool("WalkBack", false);
		}
		if (move != 0 && vertical == 0)
        {
			Anim.SetBool("Walk", true);
			Anim.SetBool("WalkFront", false);
			Anim.SetBool("WalkBack", false);
        }
		if (vertical != 0 && isUp)
        {
			Anim.SetBool("WalkBack", true);
			Anim.SetBool("Walk", false);
			Anim.SetBool("WalkFront", false);
        }
		if (vertical != 0 && !isUp)
        {
			Anim.SetBool("WalkFront", true);
			Anim.SetBool("Walk", false);
			Anim.SetBool("WalkBack", false);
        }
		
		if (move < -0.01f && facingRight)
        {
			Flip();
        }
        else if (move > 0.01f && !facingRight)
        {
            Flip();
        }
	}
	
	void Flip()
    {
        Vector3 currentScale = playerCharacterToFlip.transform.localScale;
        currentScale.x *= -1;
        playerCharacterToFlip.transform.localScale = currentScale;

        facingRight = !facingRight;
    }
	
    public void SpaceMap(InputAction.CallbackContext context)
	{
		if(Character.CurrentPin.isunlocked)
		{
			screenAspectRatio.Open();
			StartCoroutine(ToLoadNow());
		}
    }
	
    IEnumerator ToLoadNow()
    {
		yield return new WaitForSeconds(timetoload);
		Character.CurrentPin.goSceneNow();
    }
	
    public void Movement(InputAction.CallbackContext context)
	{
		move = context.ReadValue<Vector2>().x;
		vertical = context.ReadValue<Vector2>().y;
	}
	
	/// <summary>
	/// Check if the player has pressed a button
	/// </summary>
	private void CheckForInput()
	{
		if (vertical > 0f)
		{
			Character.TrySetDirection(Direction.Up);
			isUp = true;
		}
		else if(vertical < 0f)
		{
			Character.TrySetDirection(Direction.Down);
			isUp = false;
		}
		else if(move < 0f)
		{
			Character.TrySetDirection(Direction.Left);
		}
		else if(move > 0f)
		{
			Character.TrySetDirection(Direction.Right);
		}
	}

	
	/// <summary>
	/// Update the GUI text
	/// </summary>
	public void UpdateGui()
	{
		SelectedLevelText.text = string.Format(Character.CurrentPin.LevelName);
	}
}
