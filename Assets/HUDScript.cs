using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDScript : MonoBehaviour {
	private GameObject[] players;
	public GameObject p1StartImg;
	public GameObject p1Countdown;
	public GameObject p1Bombs;
	public GameObject p1Invincible;

	public GameObject p2StartImg;
	public GameObject p2Countdown;
	public GameObject p2Bombs;
	public GameObject p2Invincible;

	public GameObject p3StartImg;
	public GameObject p3Countdown;
	public GameObject p3Bombs;
	public GameObject p3Invincible;

	public GameObject p4StartImg;
	public GameObject p4Countdown;
	public GameObject p4Bombs;
	public GameObject p4Invincible;

	// Use this for initialization
	void Start () {
		players = GameObject.FindGameObjectsWithTag ("Player");
	}
	
	// Update is called once per frame
	void OnGUI () {
		players = GameObject.FindGameObjectsWithTag ("Player");

		CreatePlayer pl = Camera.main.GetComponent<CreatePlayer>();
		if (pl.player1IsActive == CreatePlayer.SummonStatus.ACTIVE) {
			p1StartImg.SetActive (false);

			for (int i = 0; i < players.Length; i++){
				PlayerScript p = players[i].GetComponent<PlayerScript>();
				if(p.myPlayer == 1){
					p1Invincible.SetActive(p.invincibilityEffect.activeSelf);
					p1Bombs.SetActive(true);
					p1Countdown.SetActive(true);
					if(p.bombCount == 0){
						p1Bombs.GetComponent<Image>().color = new Color(1f,1f,1f,.5f);
					}
					else{
						p1Bombs.GetComponent<Image>().color = Color.white;
					}
					p1Countdown.GetComponent<Text>().text = "x"+p.bombCount;
				}
				break;
			}
		} 
		else if (pl.player1IsActive != CreatePlayer.SummonStatus.ACTIVE && pl.spawntimer1 > 0) {
			p1Invincible.SetActive(false);
			p1Bombs.SetActive(false);
			p1StartImg.SetActive (false);
			//show count down timer
			p1Countdown.SetActive(true);
			p1Countdown.GetComponent<Text>().text =Mathf.FloorToInt((float)pl.spawntimer1).ToString();

		}
		else {
			p1StartImg.SetActive (true);
			//show count down timer
			p1Countdown.SetActive(false);
		}

		if (pl.player2IsActive == CreatePlayer.SummonStatus.ACTIVE) {
			p2StartImg.SetActive (false);
			for (int i = 0; i < players.Length; i++){
				PlayerScript p = players[i].GetComponent<PlayerScript>();
				if(p.myPlayer == 2){
					p2Invincible.SetActive(p.invincibilityEffect.activeSelf);
					p2Bombs.SetActive(true);
					p2Countdown.SetActive(true);
					if(p.bombCount == 0){
						p2Bombs.GetComponent<Image>().color = new Color(1f,1f,1f,.5f);
					}
					else{
						p2Bombs.GetComponent<Image>().color = Color.white;
					}
					p2Countdown.GetComponent<Text>().text = "x"+p.bombCount;
				}
				break;
			}
		}
		else if (pl.player2IsActive != CreatePlayer.SummonStatus.ACTIVE && pl.spawntimer2 > 0) {
			p2StartImg.SetActive (false);
			p2Invincible.SetActive(false);
			p2Bombs.SetActive(false);

			//show count down timer
			p2Countdown.SetActive(true);
			p2Countdown.GetComponent<Text>().text = Mathf.FloorToInt((float)pl.spawntimer2).ToString();
		}
		else {
			p2StartImg.SetActive (true);
			//show count down timer
			p2Countdown.SetActive(false);
		}

		if (pl.player3IsActive == CreatePlayer.SummonStatus.ACTIVE) {
			p3StartImg.SetActive (false);

			for (int i = 0; i < players.Length; i++){
				PlayerScript p = players[i].GetComponent<PlayerScript>();
				if(p.myPlayer == 3){
					p3Invincible.SetActive(p.invincibilityEffect.activeSelf);
					p3Bombs.SetActive(true);
					p3Countdown.SetActive(true);
					if(p.bombCount == 0){
						p3Bombs.GetComponent<Image>().color = new Color(1f,1f,1f,.5f);
					}
					else{
						p3Bombs.GetComponent<Image>().color = Color.white;
					}
					p3Countdown.GetComponent<Text>().text = "x"+p.bombCount;
				}
				break;
			}
		}
		else if (pl.player3IsActive != CreatePlayer.SummonStatus.ACTIVE && pl.spawntimer3 > 0) {
			p3StartImg.SetActive (false);
			p3Invincible.SetActive(false);
			p3Bombs.SetActive(false);

			//show count down timer
			p3Countdown.SetActive(true);
			p3Countdown.GetComponent<Text>().text = Mathf.FloorToInt((float)pl.spawntimer3).ToString();
		}
		else {
			p3StartImg.SetActive (true);
			//show count down timer
			p3Countdown.SetActive(false);
		}

		if (pl.player4IsActive == CreatePlayer.SummonStatus.ACTIVE) {
			p4StartImg.SetActive (false);

			for (int i = 0; i < players.Length; i++){
				PlayerScript p = players[i].GetComponent<PlayerScript>();
				if(p.myPlayer == 4){
					p4Invincible.SetActive(p.invincibilityEffect.activeSelf);
					p4Bombs.SetActive(true);
					p4Countdown.SetActive(true);
					if(p.bombCount == 0){
						p4Bombs.GetComponent<Image>().color = new Color(1f,1f,1f,.5f);
					}
					else{
						p4Bombs.GetComponent<Image>().color = Color.white;
					}
					p4Countdown.GetComponent<Text>().text = "x"+p.bombCount;
				}
				break;
			}
		}
		else if (pl.player4IsActive != CreatePlayer.SummonStatus.ACTIVE && pl.spawntimer4 > 0) {
			p4StartImg.SetActive (false);
			p4Invincible.SetActive(false);
			p4Bombs.SetActive(false);

			//show count down timer
			p4Countdown.SetActive(true);
			p4Countdown.GetComponent<Text>().text = Mathf.FloorToInt((float)pl.spawntimer4).ToString();
		}
		else {
			p4StartImg.SetActive (true);
			p4Countdown.SetActive(false);
		}
	}
}
