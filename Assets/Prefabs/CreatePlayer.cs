using UnityEngine;
using System.Collections;

public class CreatePlayer : MonoBehaviour {

	public bool player1IsActive = false;
	public bool player2IsActive = false;
	public bool player3IsActive = false;
	public bool player4IsActive = false;

	public GameObject player;

	public double spawntimer1;
	public double spawntimer2;
	public double spawntimer3;
	public double spawntimer4;
	private double TimeToWait;
	private bool IsTiming = false;

	private string _isMac;
	// Use this for initialization
	void Start () 
	{
		spawntimer1 = 1;
		spawntimer2 = 1;
		spawntimer3 = 1;
		spawntimer4 = 1;
		IsTiming = true;
		TimeToWait = 6;
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
	void Update () 
	{
		if (IsTiming == true)
		{
			//Add the change in time between frames to the timer
			spawntimer1 -= Time.deltaTime;
			spawntimer2 -= Time.deltaTime;
			spawntimer3 -= Time.deltaTime;
			spawntimer4 -= Time.deltaTime;
		}

		//reset spawn timer when it gets too high
		if (spawntimer1 < 0) 
			{
				spawntimer1 = -10;
			}
		if (spawntimer2 < 0) 
			{
				spawntimer2 = -10;
			}
		if (spawntimer3 < 0) 
			{
				spawntimer3 = -10;
			}
		if (spawntimer4 < 0) 
			{
				spawntimer4 = -10;
			}
		//end reset code

		if(!player1IsActive && Input.GetButtonDown("JumpP1"+_isMac) && spawntimer1 < 0)
		{ 
			player1IsActive = true;
			PlayerScript p1 = ((GameObject)Instantiate(player, new Vector3(-6f, -3f, 20), Quaternion.identity)).GetComponent<PlayerScript>();
			p1.myPlayer = 1;
			spawntimer1 = TimeToWait;
		}
		if(!player2IsActive && Input.GetButtonDown("JumpP2"+_isMac) && spawntimer2 < 0)
		{
			player2IsActive = true;
			PlayerScript p1 = ((GameObject)Instantiate(player, new Vector3(-3f, -3f, 20), Quaternion.identity)).GetComponent<PlayerScript>();
			p1.myPlayer = 2;
			spawntimer2 = TimeToWait;
		}
		if(!player3IsActive && Input.GetButtonDown("JumpP3"+_isMac) && spawntimer3 < 0)
		{
			player3IsActive = true;
			PlayerScript p1 = ((GameObject)Instantiate(player, new Vector3(-0f, -3f, 20), Quaternion.identity)).GetComponent<PlayerScript>();
			p1.myPlayer = 3;
			spawntimer3 = TimeToWait;
		}
		if(!player4IsActive && Input.GetButtonDown("JumpP4"+_isMac) && spawntimer4 < 0)
		{
			player4IsActive = true;
			PlayerScript p1 = ((GameObject)Instantiate(player, new Vector3(3f, -3f, 20), Quaternion.identity)).GetComponent<PlayerScript>();
			p1.myPlayer = 4;
			spawntimer4 = TimeToWait;
		}
	
	
	}
}
