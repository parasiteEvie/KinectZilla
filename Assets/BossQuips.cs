using UnityEngine;
using System.Collections;

public class BossQuips : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	public void StartQuip(){
		GetComponent<Animator> ().SetBool ("SaySomething", true);
	}

	public void ResetQuip(){
		GetComponent<Animator> ().SetBool ("SaySomething", false);
	}
}
