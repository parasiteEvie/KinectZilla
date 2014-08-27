using UnityEngine;
using System.Collections;

public class CreditsControl : MonoBehaviour {
	private double timer;
	private double TimeToWait;
	private bool IsTiming = false;
	private int selected = 0;
	
	string[] buttons = new string[1] {"Return"};
	
	// Use this for initialization
	void Start () 
	{
		selected = 0;
		timer = 0;
		IsTiming = true;
		TimeToWait = 0.33;
	}
	
	int menuSelection (string[] buttonsArray, int selectedItem, string direction) 
	{
		if (direction == "up") 
		{
			if (selectedItem == 0) 
			{
				selectedItem = buttonsArray.Length - 1;
			} else 
			{
				selectedItem -= 1;
			}
		}
		if (direction == "down") {
			if (selectedItem == buttonsArray.Length - 1) 
			{
				selectedItem = 0;
			} else 
			{
				selectedItem += 1;
			}
		}
		return selectedItem;
	}
	
	
	// Update is called once per frame
	void Update () 
	{
		if (IsTiming == true)
		{
			//Add the change in time between frames to the timer
			timer += Time.deltaTime;
		}
		if (Input.GetAxis ("VerticalP1") > 0) 
		{
			if (timer > TimeToWait)
			{
				selected = menuSelection (buttons, selected, "up");
				timer = 0;
			}
		}
		if (Input.GetAxis ("VerticalP1") < 0) 
		{
			if (timer > TimeToWait)
			{				
				selected = menuSelection (buttons, selected, "down");
				timer = 0;
			}
		}
	}
	
	
	
	void OnGUI() 
	{
		GUISkin menuSkin = (GUISkin)Resources.Load("MenuGUI");
		GUI.skin = menuSkin;
		
		const int buttonWidth = 160;
		const int buttonHeight = 60;
		
		// Determine the button's place on screen
		Rect ReturnRect = new Rect((Screen.width / 2) - (buttonWidth / 2), (Screen.height / 3) - (buttonHeight / 2) + (Screen.height / 2), buttonWidth, buttonHeight);	
		
		GUI.SetNextControlName(buttons[0]);
		GUI.Button(ReturnRect, buttons[0]);
		if ((Input.GetButton ("PauseP1") || Input.GetButton("JumpP1")) && GUI.GetNameOfFocusedControl() == "Return")
		{
			// Load main level scene
			Application.LoadLevel("Menu");
		}
		GUI.FocusControl(buttons[selected]);
	}
	
}
