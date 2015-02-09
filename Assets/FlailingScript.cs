using UnityEngine;
using System.Collections;

public class FlailingScript : MonoBehaviour {

	public GameObject gunArm;

	// Use this for initialization
	void Start () {
		gunArm.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowArm(){
		gunArm.SetActive(true);
	}
	
	public void HideArm(){
		gunArm.SetActive(false);
	}
}
