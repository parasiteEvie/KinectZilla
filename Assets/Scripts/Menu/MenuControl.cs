using UnityEngine;
using System.Collections;

public class MenuControl : MonoBehaviour {

string[] buttons = new string[2] {"Start", "Credits"};
int selected = 0;
	// Use this for initialization
	void Start () 
	{
	selected = 0;		
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
		if(Input.GetAxis("VerticalP1") > 0)
		{
			selected = menuSelection(buttons, selected, "up");
		}
		if(Input.GetAxis("VerticalP1") < 0)
		{
			selected = menuSelection(buttons, selected, "down");
		}
	}
	void OnGUI()
	{
		GUISkin menuSkin = (GUISkin)Resources.Load("MenuGUI");
		GUI.skin = menuSkin;
		
		const int buttonWidth = 160;
		const int buttonHeight = 60;

		// Determine the button's place on screen
		// Center in X, 2/3 of the height in Y
		Rect StartRect = new Rect((Screen.width / 2) - (buttonWidth / 2) + (Screen.width / 3), (Screen.height / 3) - (buttonHeight / 2), buttonWidth, buttonHeight);
		Rect CreditsRect = new Rect((Screen.width / 2) - (buttonWidth / 2) + (Screen.width / 3), (Screen.height / 3) - (buttonHeight / 2) + 65, buttonWidth, buttonHeight);

		GUI.SetNextControlName(buttons[0]);
		if(GUI.Button(StartRect, buttons[0]))
		   {
			// Load main level scene
			Application.LoadLevel("Main");
		}
		GUI.SetNextControlName(buttons[1]);
		if(GUI.Button(CreditsRect, buttons[1]))
		   {
			// Load Credits Scene
			Application.LoadLevel("Credits");
		}
//		GUI.SetNextControlName(buttons[2]);
//		If(GUI.Button(new Rect(0,200,100,100), buttons[2])
//		   {
//			//when selected Exit button
//		}
		GUI.FocusControl(buttons[selected]);
	}

}

