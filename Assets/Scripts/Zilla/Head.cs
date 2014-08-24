using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using Holoville.HOTween.Core;

public class Head:MonoBehaviour {
	private Vector3 pos;

	private BodyPartControls bpc;

	// Init
	public void Awake() {
		bpc = GetComponent<BodyPartControls>();
	}

	public void LateUpdate() {
		pos = transform.position;
		// Check bounds
		if(pos.x > 26f) {
			pos.x = 26f;
		}
		if(pos.x < -26f) {
			pos.x = -26f;
		}
		if(pos.y < -10f) {
			pos.y = -10f;
		}
		if(pos.y > 18f) {
			pos.y = 18f;
		}
		// Adjust
		transform.position = pos;

		// Head animation
		if(! bpc.bodyPartClosed) {
			transform.localEulerAngles = new Vector3(0f, 0f, -15f);
		} else {
			transform.localEulerAngles = Vector3.zero;
		}
	}
}
