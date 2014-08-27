using UnityEngine;
using System.Collections;

public class CreatePlayer : MonoBehaviour {

	public bool player1IsActive = false;
	public bool player2IsActive = false;
	public bool player3IsActive = false;
	public bool player4IsActive = false;

	public GameObject player;

	private string _isMac;
	// Use this for initialization
	void Start () {
		if ( Application.platform == RuntimePlatform.WindowsEditor ||
		    Application.platform == RuntimePlatform.WindowsPlayer ||
		    Application.platform == RuntimePlatform.WindowsWebPlayer) {
			_isMac = "";
			
		}
		else{
			_isMac = "Mac";
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(!player1IsActive && Input.GetButtonDown("JumpP1"+_isMac)){; 
			player1IsActive = true;
			PlayerScript p1 = ((GameObject)Instantiate(player, new Vector3(-6f, -3f, 20), Quaternion.identity)).GetComponent<PlayerScript>();
			p1.myPlayer = 1;
		}
		if(!player2IsActive && Input.GetButtonDown("JumpP2"+_isMac)){
			player2IsActive = true;
			PlayerScript p1 = ((GameObject)Instantiate(player, new Vector3(-3f, -3f, 20), Quaternion.identity)).GetComponent<PlayerScript>();
			p1.myPlayer = 2;
		}
		if(!player3IsActive && Input.GetButtonDown("JumpP3"+_isMac)){
			player3IsActive = true;
			PlayerScript p1 = ((GameObject)Instantiate(player, new Vector3(-0f, -3f, 20), Quaternion.identity)).GetComponent<PlayerScript>();
			p1.myPlayer = 3;
		}
		if(!player4IsActive && Input.GetButtonDown("JumpP4"+_isMac)){
			player4IsActive = true;
			PlayerScript p1 = ((GameObject)Instantiate(player, new Vector3(3f, -3f, 20), Quaternion.identity)).GetComponent<PlayerScript>();
			p1.myPlayer = 4;
		}
	
	
	}
}
