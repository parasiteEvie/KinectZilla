using UnityEngine;
using System.Collections;

public class SpawnPlayersFlag : MonoBehaviour {

	public bool spawnPlayers;
	// Use this for initialization
	void Start () {
		spawnPlayers = false;
	}

	public void ResetTransition(){
		GetComponent<Animator> ().SetBool ("summoned", false);
	}

	public void ShowTruck(){
		GetComponentInChildren<SpriteRenderer> ().enabled = true;
	}
	// Update is called once per frame
	void Update () {
	
	}
}
