using UnityEngine;
using System.Collections;

public class StageStartScript : MonoBehaviour {

	public Vector3[] controllerPlayerStarts;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(transform.position, 1);

		if (controllerPlayerStarts [0] != null) {
			Gizmos.color = Color.blue;
			Gizmos.DrawSphere(controllerPlayerStarts[0], .45f);
		}

		if (controllerPlayerStarts [1] != null) {
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(controllerPlayerStarts[1], .45f);
		}
		if (controllerPlayerStarts [2] != null) {
			Gizmos.color = Color.green;
			Gizmos.DrawSphere(controllerPlayerStarts[2], .45f);
		}
		if (controllerPlayerStarts [3] != null) {
			Gizmos.color = Color.yellow;
			Gizmos.DrawSphere(controllerPlayerStarts[3], .45f);
		}
	}
}
