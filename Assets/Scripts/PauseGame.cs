using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour {
	public GUIStyle PauseStyle;

	public AudioClip otherMusic;
	public AudioClip pauseMusic;

	// Use this for initialization
	void Start()
	{
		StartCoroutine(PauseCoroutine());  
		otherMusic = audio.clip;
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
					audio.clip = otherMusic;
					audio.Play();

				} else {
					Time.timeScale = 0;
					audio.clip = pauseMusic;
					audio.Play();

				}
				
			}    
			yield return null;    
		}
	}

	void OnGUI(){
		if (Time.timeScale == 0) {
			GUI.backgroundColor = Color.black;
			GUI.Box(new Rect(0,0,Screen.width,Screen.height),"");
			GUI.Label (new Rect (0, 0, Screen.width, Screen.height), "PAUSE", PauseStyle); 
		}
	}
}