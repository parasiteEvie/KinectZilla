using UnityEngine;
using System.Collections;

public class MenuControl : MonoBehaviour {
	private double timer;
	private double TimeToWait;
	private bool IsTiming = false;
	private int selected = 0;

	public GameObject hand;

string[] buttons = new string[2] {"Start", "Credits"};

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
		Rect StartRect = new Rect((Screen.width / 2) - (buttonWidth / 2) + (Screen.width / 3), (Screen.height / 3) - (buttonHeight / 2), buttonWidth, buttonHeight);
		Rect CreditsRect = new Rect((Screen.width / 2) - (buttonWidth / 2) + (Screen.width / 3), (Screen.height / 3) - (buttonHeight / 2) + 65, buttonWidth, buttonHeight);

		GUI.SetNextControlName(buttons[0]);
		GUI.Button(StartRect, buttons[0]);
		if ((Input.GetButton ("PauseP1") || Input.GetButton("JumpP1")) && GUI.GetNameOfFocusedControl() == "Start")
			{
				// Load main level scene
				Application.LoadLevel("Main");
			}
		GUI.SetNextControlName(buttons[1]);
		GUI.Button (CreditsRect, buttons [1]);
		if ((Input.GetButton ("PauseP1") || Input.GetButton("JumpP1")) && GUI.GetNameOfFocusedControl() == "Credits")
		   {
			// Load Credits Scene
			Application.LoadLevel("Credits");
			}
		GUI.FocusControl(buttons[selected]);

		if(!Application.isLoadingLevel){

			Vector3 handScreenPoint = Camera.main.WorldToViewportPoint(hand.transform.position);
			handScreenPoint = new Vector3(handScreenPoint.x * Screen.width, Screen.height - (handScreenPoint.y * Screen.height), handScreenPoint.z);
			//hand controls override
			Hand hs = hand.GetComponent<Hand>();
			if (StartRect.Contains(new Vector2(handScreenPoint.x, handScreenPoint.y)))
			{
				if(hs.MenuSelect()){
					Application.LoadLevel("Main");
				}
			}
			else if (CreditsRect.Contains(new Vector2(handScreenPoint.x, handScreenPoint.y)))
			{
				if(hs.MenuSelect()){
					Application.LoadLevel("Credits");
				}
			}
		}


	}

}

