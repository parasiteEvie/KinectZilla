using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour {
	public GUIStyle PauseStyle;

	public AudioClip otherMusic;
	public AudioClip pauseMusic;

	public string mac;

	// Use this for initialization
	void Start()
	{
		StartCoroutine(PauseCoroutine());  
		otherMusic = audio.clip;

		mac = "Mac";
		if (Application.platform == RuntimePlatform.WindowsEditor ||
		    Application.platform == RuntimePlatform.WindowsPlayer ||
		    Application.platform == RuntimePlatform.WindowsWebPlayer) {
			mac = "";
			
		}
	}
	
	IEnumerator PauseCoroutine() {
		
	//This will pause the game by setting "timeScale" to zero. When the game is paused the background music is slowed down.

		while (true)
		{
			if (Input.GetButtonDown("PauseP1"+mac) || Input.GetButtonDown("PauseP2"+mac) || Input.GetButtonDown("PauseP3"+mac) || Input.GetButtonDown("PauseP4"+mac))
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