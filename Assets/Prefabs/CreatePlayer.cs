using UnityEngine;
using System.Collections;

public class CreatePlayer : MonoBehaviour {

	public bool player1IsActive = false;
	public bool player2IsActive = false;
	public bool player3IsActive = false;
	public bool player4IsActive = false;

	public GameObject player;

	private double spawntimer1;
	private double spawntimer2;
	private double spawntimer3;
	private double spawntimer4;
	private double TimeToWait;
	private bool IsTiming = false;

	private string _isMac;
	// Use this for initialization
	void Start () 
	{
		spawntimer1 = 3;
		spawntimer2 = 3;
		spawntimer3 = 3;
		spawntimer4 = 3;
		IsTiming = true;
		TimeToWait = 3;
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
			spawntimer1 += Time.deltaTime;
			spawntimer2 += Time.deltaTime;
			spawntimer3 += Time.deltaTime;
			spawntimer4 += Time.deltaTime;
		}

		//reset spawn timer when it gets too high
		if (spawntimer1 > 5) 
			{
				spawntimer1 = 0;
			}
		if (spawntimer2 > 5) 
			{
				spawntimer2 = 0;
			}
		if (spawntimer3 > 5) 
			{
				spawntimer3 = 0;
			}
		if (spawntimer4 > 5) 
			{
				spawntimer4 = 0;
			}
		//end reset code

		if(!player1IsActive && Input.GetButtonDown("JumpP1"+_isMac) && spawntimer1 > TimeToWait)
		{ 
			player1IsActive = true;
			PlayerScript p1 = ((GameObject)Instantiate(player, new Vector3(-6f, -3f, 20), Quaternion.identity)).GetComponent<PlayerScript>();
			p1.myPlayer = 1;
			spawntimer1 = 0;
		}
		if(!player2IsActive && Input.GetButtonDown("JumpP2"+_isMac) && spawntimer2 > TimeToWait)
		{
			player2IsActive = true;
			PlayerScript p1 = ((GameObject)Instantiate(player, new Vector3(-3f, -3f, 20), Quaternion.identity)).GetComponent<PlayerScript>();
			p1.myPlayer = 2;
			spawntimer2 = 0;
		}
		if(!player3IsActive && Input.GetButtonDown("JumpP3"+_isMac) && spawntimer3 > TimeToWait)
		{
			player3IsActive = true;
			PlayerScript p1 = ((GameObject)Instantiate(player, new Vector3(-0f, -3f, 20), Quaternion.identity)).GetComponent<PlayerScript>();
			p1.myPlayer = 3;
			spawntimer3 = 0;
		}
		if(!player4IsActive && Input.GetButtonDown("JumpP4"+_isMac) && spawntimer4 > TimeToWait)
		{
			player4IsActive = true;
			PlayerScript p1 = ((GameObject)Instantiate(player, new Vector3(3f, -3f, 20), Quaternion.identity)).GetComponent<PlayerScript>();
			p1.myPlayer = 4;
			spawntimer4 = 0;
		}
	
	
	}
}
