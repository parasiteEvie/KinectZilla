using UnityEngine;

public class CreditsButtons : MonoBehaviour 
	{
		void OnGUI()
		{
			GUISkin menuSkin = (GUISkin)Resources.Load("MenuGUI");
			GUI.skin = menuSkin;
			
			const int buttonWidth = 160;
			const int buttonHeight = 60;
			
			// Determine the button's place on screen
			// Center in X, 2/3 of the height in Y
		Rect ReturnRect = new Rect((Screen.width / 2) - (buttonWidth / 2), (Screen.height / 3) - (buttonHeight / 2) + (Screen.height / 2), buttonWidth, buttonHeight);			
			
			// Create Button
			if(GUI.Button(ReturnRect, "Return"))
			{
				// Load main level scene
				Application.LoadLevel("Menu");
			}			
		}
	}