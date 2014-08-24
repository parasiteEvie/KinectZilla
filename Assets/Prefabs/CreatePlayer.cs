using UnityEngine;
using System.Collections;

public class CreatePlayer : MonoBehaviour {

	bool player1IsActive = false;
	bool player2IsActive = false;
	bool player3IsActive = false;
	bool player4IsActive = false;

	public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!player1IsActive && Input.GetButtonDown("Fire1P1")){;
			Debug.Log ("I am player 1");
			player1IsActive = true;
			PlayerScript p1 = ((GameObject)Instantiate(player, new Vector3(-6f, -3f, 20), Quaternion.identity)).GetComponent<PlayerScript>();
			p1.myPlayer = 1;
		}
		if(!player2IsActive && Input.GetButtonDown("Fire1P2")){
			Debug.Log ("I am player 2");
			player2IsActive = true;
			PlayerScript p1 = ((GameObject)Instantiate(player, new Vector3(-3f, -3f, 20), Quaternion.identity)).GetComponent<PlayerScript>();
			p1.myPlayer = 2;
		}
		if(!player3IsActive && Input.GetButtonDown("Fire1P3")){
			Debug.Log ("I made a thing");
			player3IsActive = true;
			PlayerScript p1 = ((GameObject)Instantiate(player, new Vector3(-0f, -3f, 20), Quaternion.identity)).GetComponent<PlayerScript>();
			p1.myPlayer = 3;
		}
		if(!player4IsActive && Input.GetButtonDown("Fire1P4")){
			Debug.Log ("I made a thing");
			player4IsActive = true;
			PlayerScript p1 = ((GameObject)Instantiate(player, new Vector3(3f, -3f, 20), Quaternion.identity)).GetComponent<PlayerScript>();
			p1.myPlayer = 4;
		}
	
	
	}
}
