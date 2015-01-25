using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {
	public PedastalScript[] stage1cities;
	public PedastalScript[] stage2cities;
	public PedastalScript[] stage3cities;

	public StageStartScript[] startPositions;

	public int currentLevel = 0;

	// Use this for initialization
	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	
	// Update is called once per frame
	public bool checkForCompletion () 
	{
		PedastalScript[] stage;

		if (currentLevel == 0) {
			stage = stage1cities;
		} else if (currentLevel == 1) {
			stage = stage2cities;
		} else {
			stage = stage3cities;
		}

		bool allDone = true;
		foreach (PedastalScript city in stage) {
			if(!city.destroyed){
				allDone = false;
			}
		}
		return allDone;
		
	}

	public void AdvanceLevel(){
		if (!checkForCompletion ()) {
			return;
		}
		currentLevel++;
		if(currentLevel < 3)
			StartCoroutine (CameraMoveTo ());
	}

	public IEnumerator CameraMoveTo(){
		GameObject camera = Camera.main.gameObject;
		while (camera.transform.position != startPositions[currentLevel].transform.position) {
			camera.transform.position = Vector3.SmoothDamp(camera.transform.position, startPositions[currentLevel].transform.position, ref velocity, dampTime);
			yield return null;
		}
	}

	public IEnumerator PlayersMoveTo(){
		yield return null;
	}
}
