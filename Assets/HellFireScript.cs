using UnityEngine;
using System.Collections;

public class HellFireScript : MonoBehaviour {

	Vector3 startDelta;
	// Use this for initialization
	void Start () {
		startDelta = transform.position - Camera.main.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Camera.main.transform.position + startDelta;
	}
}
