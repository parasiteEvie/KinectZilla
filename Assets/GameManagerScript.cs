using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {
	public PedastalScript[] stage1cities;
	public PedastalScript[] stage2cities;
	public PedastalScript[] stage3cities;

	public StageStartScript[] startPositions;

	public int currentLevel = 0;

	public GameObject BossHead;
	public GameObject BossLeftHand;
	public GameObject BossRightHand;

	public GameObject HellFire;
	// Use this for initialization
	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;

	public GameObject fireTruck;

	void Awake(){
		//GetComponent<SpriteRenderer> ().enabled = false;

	}

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

		BossHead.GetComponent<Head> ().Roar ();
		if (currentLevel < 3) {
			GetComponent<Animator> ().SetTrigger ("AdvanceLevel");

			GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
			
			foreach (GameObject body in players) {
				PlayerAnimationScript pas = body.GetComponentInChildren<PlayerAnimationScript>();
				FlailingScript fs = body.GetComponentInChildren<FlailingScript>();
				
				pas.gameObject.GetComponent<SpriteRenderer>().enabled = false;
				fs.gameObject.GetComponent<SpriteRenderer>().enabled = true;
				
				fs.gameObject.GetComponent<Animator>().SetInteger("FlailColor", body.GetComponent<PlayerScript>().myPlayer);
				fs.gameObject.GetComponent<Animator>().SetTrigger("TimeToFlail");
			}

			fireTruck.GetComponent<Animator> ().SetTrigger ("AdvanceLevel");

		}
			
	}

	public void StartMovingCamera(){
		StartCoroutine (CameraMoveTo ());
	}

	public IEnumerator CameraMoveTo(){
		GameObject camera = Camera.main.gameObject;
		while (Vector3.Distance(camera.transform.position, startPositions[currentLevel].transform.position) > 0.1f) {
			camera.transform.position = Vector3.SmoothDamp(camera.transform.position, startPositions[currentLevel].transform.position, ref velocity, dampTime);
			yield return null;
		}
		camera.transform.position = startPositions[currentLevel].transform.position;
		BossLeftHand.GetComponent<Hand>().UpdatePosition();
		BossRightHand.GetComponent<Hand>().UpdatePosition();
		BossHead.GetComponent<Head>().UpdatePosition();
	
	}

	public void ShowAnimation(){
		GetComponent<SpriteRenderer> ().enabled = true;
	}

	public void HideAnimation(){
		GetComponent<SpriteRenderer> ().enabled = false;
	}
	
	public void MoveToBackground(){
		GetComponent<SpriteRenderer> ().sortingOrder = -10;
	}

	public void MoveToForeground(){
		GetComponent<SpriteRenderer> ().sortingOrder = 100;
	}
	public void DisableKinecter(){
		BossHead.SetActive (false);
		BossLeftHand.SetActive (false);
		BossRightHand.SetActive (false);
		HellFire.SetActive (true);
	}
	public void EnableKinecter(){
		BossHead.SetActive (true);
		BossLeftHand.SetActive (true);
		BossRightHand.SetActive (true);
		HellFire.SetActive (false);

		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject body in players) {
			PlayerAnimationScript pas = body.GetComponentInChildren<PlayerAnimationScript>();
			FlailingScript fs = body.GetComponentInChildren<FlailingScript>();
			
			pas.gameObject.GetComponent<SpriteRenderer>().enabled = true;
			fs.gameObject.GetComponent<SpriteRenderer>().enabled = false;

			Vector3 v =  startPositions[currentLevel].controllerPlayerStarts[body.GetComponent<PlayerScript>().myPlayer - 1];
			body.GetComponent<PlayerScript>().warpPosition = new Vector3(v.x, v.y, v.z);

			Debug.Log("player number is: "+startPositions[currentLevel].controllerPlayerStarts[body.GetComponent<PlayerScript>().myPlayer - 1].ToString());
		}
	}
}
