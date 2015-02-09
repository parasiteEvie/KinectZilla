using UnityEngine;
using System.Collections;

public class CreatePlayer : MonoBehaviour {

	public enum SummonStatus
		{
			INACTIVE,
			SUMMONED,
			ACTIVE,
		}
	public SummonStatus player1IsActive = SummonStatus.INACTIVE;
	public SummonStatus player2IsActive = SummonStatus.INACTIVE;
	public SummonStatus player3IsActive = SummonStatus.INACTIVE;
	public SummonStatus player4IsActive = SummonStatus.INACTIVE;

	public GameObject player;
	public GameObject FireTruck;

	public double spawntimer1;
	public double spawntimer2;
	public double spawntimer3;
	public double spawntimer4;
	private double TimeToWait;
	private bool IsTiming = false;

	public Vector3[] spawnLocations;

	private GameObject gameManager;
	private int currentLevel;

	private string _isMac;
	// Use this for initialization
	void Start () 
	{
		gameManager = GameObject.Find("GAME MANAGER");
		spawntimer1 = 1;
		spawntimer2 = 1;
		spawntimer3 = 1;
		spawntimer4 = 1;
		IsTiming = true;
		TimeToWait = 1.5;
		if ( Application.platform == RuntimePlatform.WindowsEditor ||
		    Application.platform == RuntimePlatform.WindowsPlayer ||
		    Application.platform == RuntimePlatform.WindowsWebPlayer) {
			_isMac = "";
			
		}
		else{
			_isMac = "Mac";
		}
	}
	public void SummonFireTruck(){
		FireTruck.GetComponent<Animator>().SetBool("summoned", true);
	}
	// Update is called once per frame
	void Update () 
	{
		if (IsTiming == true)
		{
			//Add the change in time between frames to the timer

			if(FireTruck.GetComponent<SpawnPlayersFlag>().spawnPlayers == true){
				currentLevel = gameManager.GetComponent<GameManagerScript>().currentLevel;
				if (spawntimer1 < 0 && player1IsActive == SummonStatus.SUMMONED) 
				{
					player1IsActive = SummonStatus.ACTIVE;
					PlayerScript p1 = ((GameObject)Instantiate(player, spawnLocations[currentLevel] , Quaternion.identity)).GetComponent<PlayerScript>();
					p1.myPlayer = 1;
					spawntimer1 = TimeToWait;
				}
				if (spawntimer2 < 0 && player2IsActive == SummonStatus.SUMMONED) 
				{
					player2IsActive = SummonStatus.ACTIVE;
					PlayerScript p1 = ((GameObject)Instantiate(player, spawnLocations[currentLevel], Quaternion.identity)).GetComponent<PlayerScript>();
					p1.myPlayer = 2;
					spawntimer2 = TimeToWait;
				}
				if (spawntimer3 < 0 && player3IsActive == SummonStatus.SUMMONED) 
				{
					player3IsActive = SummonStatus.ACTIVE;
					PlayerScript p1 = ((GameObject)Instantiate(player, spawnLocations[currentLevel], Quaternion.identity)).GetComponent<PlayerScript>();
					p1.myPlayer = 3;
					spawntimer3 = TimeToWait;
				}
				if (spawntimer4 < 0 && player4IsActive == SummonStatus.SUMMONED) 
				{
					player4IsActive = SummonStatus.ACTIVE;
					PlayerScript p1 = ((GameObject)Instantiate(player, spawnLocations[currentLevel], Quaternion.identity)).GetComponent<PlayerScript>();
					p1.myPlayer = 4;
					spawntimer4 = TimeToWait;
				}

			}
		}

		spawntimer1 -= Time.deltaTime;
		spawntimer2 -= Time.deltaTime;
		spawntimer3 -= Time.deltaTime;
		spawntimer4 -= Time.deltaTime;

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

		if(player1IsActive == SummonStatus.INACTIVE && Input.GetButtonDown("JumpP1"+_isMac) && spawntimer1 < 0)
		{ 
			player1IsActive = SummonStatus.SUMMONED;
			FireTruck.GetComponent<Animator>().SetBool("summoned", true);
		}
		if(player2IsActive == SummonStatus.INACTIVE && Input.GetButtonDown("JumpP2"+_isMac) && spawntimer2 < 0)
		{
			player2IsActive = SummonStatus.SUMMONED;
			FireTruck.GetComponent<Animator>().SetBool("summoned", true);
		}
		if(player3IsActive == SummonStatus.INACTIVE && Input.GetButtonDown("JumpP3"+_isMac) && spawntimer3 < 0)
		{
			player3IsActive = SummonStatus.SUMMONED;
			FireTruck.GetComponent<Animator>().SetBool("summoned", true);
		}
		if(player4IsActive == SummonStatus.INACTIVE && Input.GetButtonDown("JumpP4"+_isMac) && spawntimer4 < 0)
		{
			player4IsActive = SummonStatus.SUMMONED;
			FireTruck.GetComponent<Animator>().SetBool("summoned", true);
		}
	
	
	}
}
