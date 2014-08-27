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
			if (Input.GetButtonDown("PauseP1") || Input.GetButtonDown("PauseP2") || Input.GetButtonDown("PauseP3") || Input.GetButtonDown("PauseP4"))
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