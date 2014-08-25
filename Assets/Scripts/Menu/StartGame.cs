using UnityEngine;

public class StartGame : MonoBehaviour
{
	void OnGUI()
	{
		const int buttonWidth = 84;
		const int buttonHeight = 40;
		
		// Determine the button's place on screen
		// Center in X, 2/3 of the height in Y
		Rect buttonRect = new Rect(Screen.width / 2 - (buttonWidth / 2), (2 * Screen.height / 3) - (buttonHeight / 2), buttonWidth, buttonHeight);
		
		//Custom Button Formatting
		GUIStyle customButton = new GUIStyle("Start");
		customButton.fontSize = 32;

		// Draw a button to start the game
		if(GUI.Button(buttonRect, "Start"))
		{
			// On Click, load the first level.
			// "Stage1" is the name of the first scene we created.
			Application.LoadLevel("Main");
		}
	}
}