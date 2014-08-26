using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour {

	// Use this for initialization
	void Start()
	{
		StartCoroutine(PauseCoroutine());    
	}
	
	IEnumerator PauseCoroutine() {
		
	//This will pause the game by setting "timeScale" to zero. When the game is paused the background music is slowed down.

		while (true)
		{
			if (Input.GetButtonDown("Pause1") || Input.GetButtonDown("Pause2") || Input.GetButtonDown("Pause3") || Input.GetButtonDown("Pause4"))
			{
				if (Time.timeScale == 0)
				{
					Time.timeScale = 1;
					audio.pitch = 1f;  
				} else {
					Time.timeScale = 0;
					audio.pitch = 0.75f;
				}
				
			}    
			yield return null;    
		}
	}
}