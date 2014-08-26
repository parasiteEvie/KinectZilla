using UnityEngine;

public class MainMenuButtons : MonoBehaviour

{
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


		// Create Button
		if(GUI.Button(StartRect, "Start"))
		{
			// Load main level scene
			Application.LoadLevel("Main");
		}
		// Create Button
		if(GUI.Button(CreditsRect, "Credits"))
		{
			// Load Credits Scene
			Application.LoadLevel("Credits");
		}

	}
}