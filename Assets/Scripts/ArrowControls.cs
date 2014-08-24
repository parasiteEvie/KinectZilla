using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using Holoville.HOTween.Core;

public class ArrowControls:MonoBehaviour {
	public bool isLeftHand;

	private Vector3 vel = Vector3.zero;

	public void Update() {
		if(isLeftHand) {
			vel.x = Input.GetAxis("HorizontalLeft");
			vel.y = Input.GetAxis("VerticalLeft");
		} else {
			vel.x = Input.GetAxis("HorizontalRight");
			vel.y = Input.GetAxis("VerticalRight");
		}

		rigidbody.velocity = vel * 50f;
	}

	// Draw Gizmo
	public void OnDrawGizmos() {
		Gizmos.DrawSphere(transform.position, 1f);
	}
}
